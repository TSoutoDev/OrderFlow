using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;
using OrderFlow.Domain.ValueObjects;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public sealed class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(orderItem => orderItem.Id);
        builder.Property(orderItem => orderItem.ProductId).IsRequired();
        builder.Property(orderItem => orderItem.ProductName).IsRequired().HasMaxLength(150);
        builder.Property(orderItem => orderItem.Quantity).IsRequired();
        builder.OwnsOne(orderItem => orderItem.UnitPrice,
            money =>
            {
                money.Property(value => value.Amount)
                    .HasColumnName("UnitPrice")
                    .HasPrecision(18, 2)
                    .IsRequired();

                money.Property(value => value.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

        builder.Ignore(orderItem => orderItem.Total);
    }
}