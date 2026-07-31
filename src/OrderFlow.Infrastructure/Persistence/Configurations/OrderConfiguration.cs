using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(order => order.Id);

        builder.Property(order => order.OrderNumber).IsRequired().HasMaxLength(50);
        builder.Property(order => order.CustomerId).IsRequired();
        builder.Property(order => order.Status).IsRequired().HasConversion<int>();
        builder.Property(order => order.CreatedAt).IsRequired();

        builder.Ignore(order => order.TotalItems);
        builder.Ignore(order => order.TotalAmount);

        builder.HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}