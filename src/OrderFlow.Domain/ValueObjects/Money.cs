using OrderFlow.Domain.Common;
using OrderFlow.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OrderFlow.Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get;}
        public string  Currency { get;}

        private Money()
        {
            Currency = string.Empty;
        }

        public Money(decimal amount, string currency)
        {
            if(amount < 0) 
            {
                throw new DomainException("O valor monetário não pode ser negativo.");
            }

            if (string.IsNullOrEmpty(currency))
            {
                throw new DomainException("A moeda deve ser informada.");
            }

            Amount = decimal.Round(amount, 2);
            Currency = currency.Trim().ToUpperInvariant();
        }

        public static Money Zero(string currency)
        {
            return new Money(0, currency);
        }

        public Money Add(Money other)
        {
            ValidateSameCurrency(other);

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException( "A quantidade deve ser maior que zero.");
            }

            return new Money(
                Amount * quantity,
                Currency);
        }

        private void ValidateSameCurrency(Money other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (Currency != other.Currency)
            {
                throw new DomainException("Não é possível operar valores de moedas diferentes.");
            }
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:N2}";
        }
    }
}
