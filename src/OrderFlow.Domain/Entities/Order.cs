using OrderFlow.Domain.Common;
using OrderFlow.Domain.Enums;
using OrderFlow.Domain.Exceptions;
using OrderFlow.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace OrderFlow.Domain.Entities
{
    public sealed class Order : AggregateRoot
    {
        private readonly List<OrderItem> _items = new();

        public string OrderNumber { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }


        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public int TotalItems => _items.Sum(item => item.Quantity);

        public Money TotalAmount
        {
            get
            {
                var total = Money.Zero("BRL");

                foreach (var item in _items)
                {
                    total = total.Add(item.Total);
                }
                return total;
            }
        }

        private Order(string orderNumber, Guid customerId)
        {
            ValidateOrderNumber(orderNumber);
            ValidateCustomerId(customerId);

            OrderNumber = orderNumber.Trim();
            CustomerId = customerId;
            Status = OrderStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }


        public static Order Create(string orderNumber, Guid customerId)
        {
            return new Order(orderNumber, customerId);
        }

        public void AddItem(OrderItem item)
        {
            EnsureOrderCanBeChanged();

            ArgumentNullException.ThrowIfNull(item);

            var existeItem = _items.FirstOrDefault(currentItem => currentItem.ProductId == item.ProductId);

            if (existeItem is not null)
            {
                throw new DomainException("O produto já foi adicionado ao pedido.");
            }
            _items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            EnsureOrderCanBeChanged();
            var item = _items.FirstOrDefault(currentItem => currentItem.Id == itemId);

            if (item is null)
            {
                throw new DomainException("O item informado não foi encontrado no pedido.");
            }
            _items.Remove(item);
        }
        public void ChangeItemQuantity(Guid itemId, int quantity)
        {
            EnsureOrderCanBeChanged();

            var item = _items.FirstOrDefault(currentItem => currentItem.Id == itemId);

            if (item is null)
            {
                throw new DomainException("O item informado não foi encontrado no pedido.");
            }
            item.ChangeQuantity(quantity);
        }
        private void EnsureOrderCanBeChanged()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new DomainException("Somente pedidos pendentes podem ser alterados.");
            }
        }

        private static void ValidateOrderNumber(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                throw new DomainException("O número do pedido é obrigatório.");
            }
        }

        private static void ValidateCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new DomainException("O identificador do cliente é obrigatório.");
            }
        }

        //Só é possível iniciar o processamento de um pedido que esteja pendente.
        public void StartProcessing()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new DomainException("Somente pedidos pendentes podem iniciar o processamento.");
            }

            Status = OrderStatus.Processing;
        }

        //Somente um pedido que esteja em processamento pode ser concluído.
        public void Complete()
        {
            if (Status != OrderStatus.Processing)
            {
                throw new DomainException("Somente pedidos em processamento podem ser concluídos.");
            }

            Status = OrderStatus.Completed;
        }

        //Somente pedidos em processamento podem ser marcados como falha.
        public void Fail()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new DomainException("Somente pedidos em processamento podem ser marcados como falha.");
            }

            Status = OrderStatus.Failed;
        }

        //Enviar para Dead Letter
        public void MoveToDeadLetter()
        {
            if (Status != OrderStatus.Failed)
            {
                throw new DomainException("Somente pedidos com falha podem ser enviados para Dead Letter.");
            }
            Status = OrderStatus.DeadLetter;
        }
    }
}
