using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => new { oi.IdOrder, oi.IdMenu, oi.IdDish });

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.IdOrder);

            builder.HasOne(oi => oi.Menu)
                .WithMany()
                .HasForeignKey(oi => oi.IdMenu);

            builder.HasOne(oi => oi.Dish)
                .WithMany()
                .HasForeignKey(oi => oi.IdDish);

            builder.Property(oi => oi.Quantity)
                .IsRequired();
        }
    }
}
