using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class DescuentoConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Descuento");
            builder.HasKey(d => d.IdDescuento);

            builder.HasMany(d => d.Recibos)
                .WithOne(r => r.descuento)
                .HasForeignKey(r => r.IdDescuento)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
