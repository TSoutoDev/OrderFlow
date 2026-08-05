using MediatR;
using OrderFlow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Application.Commands.Orders.CancelOrder
{
    public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await  _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order is null) 
            {
                throw new KeyNotFoundException("O pedido informado não foi encontrado.");
            }
            
            order.Cancel();

            await _orderRepository.SaveChangesAsync(cancellationToken);

        }
    }
}
