using MediatR;

namespace OrderFlow.Application.Commands.Orders.UpdateOrder
{
    public sealed record UpdateOrderCommand(Guid Id, string OrderNumber, Guid CustomerId) : IRequest;
}
