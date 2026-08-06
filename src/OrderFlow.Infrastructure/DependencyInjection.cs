using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Application.Interfaces;
using OrderFlow.Infrastructure.Configuration;
using OrderFlow.Infrastructure.Messaging;
using OrderFlow.Infrastructure.Persistence;
using OrderFlow.Infrastructure.Persistence.Repositories;


namespace OrderFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =  configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("A connection string 'DefaultConnection' não foi configurada.");

        services.AddDbContext<OrderFlowDbContext>(options =>  options.UseSqlServer(connectionString));
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IIntegrationEventPublisher, RabbitMqIntegrationEventPublisher>();

        return services;
    }
}