using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Exceptions;
using OrderFlow.Domain.ValueObjects;
using Xunit.Sdk;

namespace OrderFlow.UnitTests.Domain.Entities
{
    public sealed class OrderItemTests
    {
        [Fact]
        public void ChangeQuantity_ShouldUpdateTotal()
        {
            //arrange
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));

            //Act
            item.ChangeQuantity(5);

            //Asserts
            Assert.Equal(5, item.Quantity);
            Assert.Equal(new Money(500m, "BRL"), item.Total);
        }

        [Fact]
        public void ChangeUnitPrice_ShouldUpdateTotal()
        {
            //arrange
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));

            //Act
            item.ChangeUnitPrice(new Money(150m, "BRL"));

            //Asserts
            Assert.Equal(new Money(150m, "BRL"), item.UnitPrice);
            Assert.Equal(new Money(300m, "BRL"), item.Total);
        }

        [Fact]
        public void ChangeQuantity_ShouldThrowDomainException_WhenQuantityIsLessThanOrEqualToZero()
        {
            //arrange
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));

            //Act && //Asserts
            var exception = Assert.Throws<DomainException>(() =>
            {
                item.ChangeQuantity(0);
            });

            Assert.Equal("A quantidade do produto deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void ChangeUnitPrice_ShouldThrowArgumentNullException_WhenUnitPriceIsNull()
        {
            //arrange
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));

            //Act && //Asserts
           var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                item.ChangeUnitPrice(null);
            });

            Assert.Equal("unitPrice", exception.ParamName);
        }

        [Fact]
        public void Constructor_ShouldThrowDomainException_WhenProductNameIsEmpty()
        {
            //arrange  && //Act && //Asserts
            var exception = Assert.Throws<DomainException>(() =>
            {
                 new OrderItem(Guid.NewGuid(), "", 2, new Money(100m, "BRL"));
            });

            Assert.Equal("O nome do produto é obrigatório.", exception.Message);
        }
    }
}
