using MediatR;

namespace OrderFlow.Application.Commands.Orders.CancelOrder
{
    public sealed record CancelOrderCommand(Guid Id) : IRequest;
}
