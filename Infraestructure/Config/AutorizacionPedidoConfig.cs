using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class AutorizacionPedidoConfig : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            builder.ToTable("AutorizacionPedido");

            builder.HasKey(ap => new { ap.IdPedido, ap.IdPersonal });
            builder.HasOne(ap => ap.Pedido)
                .WithOne(p => p.AutorizacionPedido)
                .HasForeignKey<Authorization>(ap => ap.IdPedido);

            builder.HasOne(ap => ap.Personal)
                .WithMany()
                .HasForeignKey(ap => ap.IdPersonal);
        }
    }
}
