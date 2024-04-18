using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class DiscountConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.IdDiscount);

            builder.Property(d => d.StartDate).IsRequired();
            builder.Property(d => d.Percentage).IsRequired();

            builder.HasMany(d => d.Receipts)
                .WithOne(r => r.Discount)
                .HasForeignKey(r => r.IdDiscount)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
