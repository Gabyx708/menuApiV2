using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class PedidoPorMenuPlatilloConfig : IEntityTypeConfiguration<PedidoPorMenuPlatillo>
    {
        public void Configure(EntityTypeBuilder<PedidoPorMenuPlatillo> builder)
        {
            builder.ToTable("PedidoPorMenuPlatillo");
            builder.HasKey(pmp => new { pmp.IdPedido, pmp.IdMenuPlatillo });

            builder.HasOne(pmp => pmp.Pedido)
                .WithMany(p => p.PedidosPorMenuPlatillo)
                .HasForeignKey(pmp => pmp.IdPedido);

            builder.HasOne(pmp => pmp.MenuPlatillo)
                .WithMany(mp => mp.PedidosPorMenuPlatillo)
                .HasForeignKey(pmp => pmp.IdMenuPlatillo);
        }
    }
}
