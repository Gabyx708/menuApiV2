using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class DescuentoConfig : IEntityTypeConfiguration<Descuento>
    {
        public void Configure(EntityTypeBuilder<Descuento> builder)
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
