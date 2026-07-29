using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Enums;
using OrderFlow.Domain.Exceptions;
using OrderFlow.Domain.ValueObjects;

namespace OrderFlow.UnitTests.Domain.Entities
{
    public sealed class OrderTests
    {
        [Fact]
        public void Create_ShouldInitializeOrderWithPendingStatus()
        {
            //Arrange
            var customerId = Guid.NewGuid();

            //Act
            var order = Order.Create("ORD-001", customerId);

            //Assert
            Assert.Equal(OrderStatus.Pending, order.Status);
        }


        [Fact] //Um pedido válido deve entrar em processamento quando StartProcessing() for chamado.
        public void StartProcessing_ShouldChangeStatusToProcessing()
        {
            //Arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());

            //Act
            order.StartProcessing();

            //Assert
            Assert.Equal(OrderStatus.Processing, order.Status);
        }

        [Fact]
        public void StartProcessing_ShouldThrowDomainException_WhenOrderIsNotPending()
        {
            //Arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());
            order.StartProcessing();
            order.Complete();

            //Act && //Assert
            Assert.Throws<DomainException>(() 
                => { order.StartProcessing(); });

        }

        [Fact]
        public void AddItem_ShouldThrowDomainException_WhenProductAlreadyExists()
        {
            //Arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());
            var productId = Guid.NewGuid();

            var item1 = new OrderItem(productId, "notebook", 1, new Money(3500m, "BRL"));
            var item2 = new OrderItem(productId, "notebook", 2, new Money(3500m, "BRL"));
            
            order.AddItem(item1);

            //Act 
            Action action = () => order.AddItem(item2);

            //Assert
            Assert.Throws<DomainException>(action);
        }

        [Fact]
        public void AddItem_ShouldUpdateOrderTotals()
        {
            // Arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));
            // Act
            order.AddItem(item);

            // Assert
            Assert.Equal(2, order.TotalItems);
            Assert.Equal(200m, order.TotalAmount.Amount);
            Assert.Equal( new Money(200m, "BRL"), order.TotalAmount);
        }

        [Fact]
        public void RemoveItem_ShouldUpdateOrderTotals()
        {
            //Arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());
            var item = new OrderItem(Guid.NewGuid(), "Mouse", 2, new Money(100m, "BRL"));
            var item2 = new OrderItem(Guid.NewGuid(), "teclado", 1, new Money(300m, "BRL"));

            order.AddItem(item);
            order.AddItem(item2);

            // Act
            order.RemoveItem(item2.Id);

            //Assert
            Assert.Single(order.Items);
            Assert.Equal(2,order.TotalItems);
            Assert.Equal(new Money (200m, "BRL"),order.TotalAmount);
        }

        
        [Fact]// Não deve ser possível remover um item que não existe no pedido
        public void RemoveItem_ShouldThrowDomainException_WhenItemDoesNotExist()
        {
            //arrange
            var order = Order.Create("ORD-001", Guid.NewGuid());
            var naoexisteNoPedido = Guid.NewGuid();

            //Act
            Action action = () => order.RemoveItem(naoexisteNoPedido);

            //Assert
            Assert.Throws<DomainException>(() => action());
        }
    }
}
