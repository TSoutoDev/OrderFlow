using OrderFlow.Domain.Common;
using OrderFlow.Domain.Exceptions;
using OrderFlow.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Domain.Entities
{
    public sealed class OrderItem : Entity
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        public Money Total => UnitPrice.Multiply(Quantity);

        private OrderItem()
        {
            ProductName = string.Empty;
            UnitPrice = Money.Zero("BRL");
        }

        public OrderItem (Guid productId, string productName, int quantity, Money unitPrice)
        {
            ValidateProductId(productId);
            ValidateProductName(productName);
            ValidateQuantity(quantity);

            ArgumentNullException.ThrowIfNull(unitPrice);

            ProductId = productId;
            ProductName = productName.Trim();
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void ChangeQuantity(int quantity)
        {
            ValidateQuantity(quantity);

            Quantity = quantity;
        }
        public void ChangeUnitPrice(Money unitPrice)
        {
            ArgumentNullException.ThrowIfNull(unitPrice);

            UnitPrice = unitPrice;
        }

        private static void ValidateProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new DomainException("O identificador do produto é obrigatório.");
            }
        }

        private static void ValidateProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new DomainException("O nome do produto é obrigatório.");
            }
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("A quantidade do produto deve ser maior que zero.");
            }
        }
    }
}
