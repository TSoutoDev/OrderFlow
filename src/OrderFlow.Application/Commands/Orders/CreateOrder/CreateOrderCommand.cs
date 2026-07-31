using MediatR;
using OrderFlow.Application.DTOs;

namespace OrderFlow.Application.Commands.Orders.CreateOrder
{
    public sealed record CreateOrderCommand(string OrderNumber, Guid CustomerId,
        IReadOnlyCollection<CreateOrderItemDto> Items) : IRequest<Guid>;
}
