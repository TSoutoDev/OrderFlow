using MediatR;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Application.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new KeyNotFoundException("O pedido informado não foi encontrado.");
            }

            order.Update(request.OrderNumber, request.CustomerId);

            await _orderRepository.SaveChangesAsync(cancellationToken);

        }
    }
}
