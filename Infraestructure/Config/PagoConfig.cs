using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class PagoConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            long result;

            builder.ToTable("Pagos");
            builder.HasKey(p => p.NumeroPago);

            builder.Property(p => p.NumeroPago)
            .ValueGeneratedOnAdd();


            builder.HasMany(p => p.Recibos)
           .WithOne(r => r.pago)
           .HasForeignKey(r => r.NumeroPago);

            // Relación uno a muchos con Personal
            builder.HasOne(p => p.Personal)
                .WithMany(p => p.pagos) // Propiedad de navegación en Personal
                .HasForeignKey(p => p.idPersonal);
        }
    }
}
