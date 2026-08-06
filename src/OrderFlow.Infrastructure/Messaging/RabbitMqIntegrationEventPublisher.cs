using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderFlow.Application.Interfaces;
using OrderFlow.Infrastructure.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderFlow.Infrastructure.Messaging
{
    public sealed class RabbitMqIntegrationEventPublisher : IIntegrationEventPublisher
    {
        private readonly RabbitMqOptions _options;
        private readonly ILogger<RabbitMqIntegrationEventPublisher> _logger;

        public RabbitMqIntegrationEventPublisher(IOptions<RabbitMqOptions> options, ILogger<RabbitMqIntegrationEventPublisher> logger)
        {
            _options = options.Value;
            _logger = logger;
        }
        public async Task PublishAsync<T>(T integratioEvent, CancellationToken cancellationToken = default) where T : class
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
            };

            await using var connection = await factory.CreateConnectionAsync(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            await channel.QueueDeclareAsync(
                queue: _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken
                );

            var message = JsonSerializer.Serialize(integratioEvent);

            var body = Encoding.UTF8.GetBytes(message);

            var properties = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: _options.QueueName,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken
                );

            _logger.LogInformation("Evento {EventType} publicado na fila {QueueName}.", typeof(T).Name, _options.QueueName);
        }
    }
}
