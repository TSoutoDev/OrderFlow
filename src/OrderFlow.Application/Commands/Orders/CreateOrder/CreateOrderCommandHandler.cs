using MediatR;
using OrderFlow.Application.Interfaces;
using OrderFlow.Domain.Entities;
using OrderFlow.Domain.ValueObjects;

namespace OrderFlow.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = Order.Create(request.OrderNumber, request.CustomerId);

            foreach (var item in request.Items)
            {
                var unitPrice = new Money(item.UnitPrice, "BRL");
                var orderItem = new OrderItem(item.ProductId,
                item.ProductName,
                item.Quantity,
                unitPrice);

                order.AddItem(orderItem);
            }

            await _orderRepository.AddAsync(order, cancellationToken);

            return order.Id;
        }
    }
}
