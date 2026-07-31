namespace OrderFlow.Application.DTOs
{
    public sealed class CreateOrderItemDto
    {
        public Guid ProductId { get; set;  }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
