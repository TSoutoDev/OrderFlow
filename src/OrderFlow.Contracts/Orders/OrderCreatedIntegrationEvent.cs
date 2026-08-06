namespace OrderFlow.Contracts.Events.Orders;

public sealed record OrderCreatedIntegrationEvent(
    Guid OrderId,
    string OrderNumber,
    Guid CustomerId,
    DateTime CreatedAt);