using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.IdOrder);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.State)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.StateCode);
        }
    }
}
