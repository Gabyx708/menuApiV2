using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class AutorizacionPedidoConfig : IEntityTypeConfiguration<AutorizacionPedido>
    {
        public void Configure(EntityTypeBuilder<AutorizacionPedido> builder)
        {
            builder.ToTable("AutorizacionPedido");

            builder.HasKey(ap => new { ap.IdPedido, ap.IdPersonal });
            builder.HasOne(ap => ap.Pedido)
                .WithOne(p => p.AutorizacionPedido)
                .HasForeignKey<AutorizacionPedido>(ap => ap.IdPedido);

            builder.HasOne(ap => ap.Personal)
                .WithMany()
                .HasForeignKey(ap => ap.IdPersonal);
        }
    }
}
