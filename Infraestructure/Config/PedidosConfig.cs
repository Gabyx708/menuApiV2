using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class PedidosConfig : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(p => p.IdPedido);

            builder.HasOne(pe => pe.Personal)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.IdPersonal)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
