namespace OrderFlow.Application.Interfaces
{
    public interface IIntegrationEventPublisher
    {
        //A aplicação precisa publicará uma mensagem, mas não precisa saber qual tecnologia realizará a publicação (PublishAsync<T>)
        Task PublishAsync<T>(T integratioEvent,CancellationToken cancellationToken = default)
            where T : class;
    }
}
