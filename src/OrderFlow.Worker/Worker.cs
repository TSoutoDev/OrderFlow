using Microsoft.Extensions.Options;
using OrderFlow.Contracts.Events.Orders;
using OrderFlow.Worker.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderFlow.Worker;

public class Worker(ILogger<Worker> logger, IOptions<RabbitMqOptions> options) : BackgroundService
{
    private readonly RabbitMqOptions _options = options.Value;
    private const string RetryHeader = "x-retry-count";
    private const string DeadLetterQueue = "order-created-dlq";
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Host,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        await using var connection = await factory.CreateConnectionAsync(stoppingToken);

        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        //DLQ:
        await channel.QueueDeclareAsync(
            queue: DeadLetterQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                var integrationEvent = JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(message);

                if (integrationEvent is null)
                {
                    throw new InvalidOperationException("Não foi possível desserializar a mensagem.");
                }

                logger.LogInformation("Pedido recebido. Id: {OrderId} | Número: {OrderNumber}",
                    integrationEvent.OrderId,
                    integrationEvent.OrderNumber);

                if (integrationEvent.OrderNumber == "ERRO")
                {
                    throw new InvalidOperationException(
                        "Erro simulado para testar NACK.");
                }

                await channel.BasicAckAsync(
                    deliveryTag: eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: stoppingToken);

                logger.LogInformation("ACK enviado para o pedido {OrderId}.", integrationEvent.OrderId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar mensagem.");

                var retryCount = 0;

                if (eventArgs.BasicProperties.Headers is not null && eventArgs.BasicProperties.Headers.TryGetValue(RetryHeader, out var retryValue))
                {
                    retryCount = Convert.ToInt32(retryValue);
                }

                //republicar
                if (retryCount < 3)
                { 
                    var properties = new BasicProperties
                    {
                        ContentType = "application/json",
                        DeliveryMode = DeliveryModes.Persistent,
                        Headers = new Dictionary<string, object?>
                        {
                            [RetryHeader] = retryCount + 1
                        }
                    };

                    await channel.BasicPublishAsync(
                        exchange: string.Empty,
                        routingKey: _options.QueueName,
                        mandatory: false,
                        basicProperties: properties,
                        body: eventArgs.Body,
                        cancellationToken: stoppingToken);

                    await channel.BasicAckAsync(
                        deliveryTag: eventArgs.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);

                    logger.LogWarning( "Mensagem reenviada para retry. Tentativa: {RetryCount}",  retryCount + 1);
                }
                else
                {
                    logger.LogError("Limite de retries atingido. Enviando mensagem para a DLQ.");

                    var dlqProperties = new BasicProperties
                    {
                        ContentType = "application/json",
                        DeliveryMode = DeliveryModes.Persistent,
                        Headers = new Dictionary<string, object?>
                        {
                            [RetryHeader] = retryCount
                        }
                    };

                    await channel.BasicPublishAsync(
                       exchange: string.Empty,
                       routingKey: DeadLetterQueue,
                       mandatory: false,
                       basicProperties: dlqProperties,
                       body: eventArgs.Body,
                       cancellationToken: stoppingToken);

                    await channel.BasicAckAsync(
                     deliveryTag: eventArgs.DeliveryTag,
                     multiple: false,
                     cancellationToken: stoppingToken);

                    logger.LogWarning("Mensagem enviada para a DLQ: {DeadLetterQueue}", DeadLetterQueue);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: _options.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        logger.LogInformation("Worker conectado ao RabbitMQ. Fila: {QueueName}", _options.QueueName);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}