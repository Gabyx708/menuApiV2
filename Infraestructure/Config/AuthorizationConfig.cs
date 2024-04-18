using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class AuthorizationConfig : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {

            builder.HasKey(a => new { a.IdUser, a.IdOrder });
            builder.HasOne(a => a.Order)
                .WithOne(o => o.Authorization)
                .HasForeignKey<Authorization>(a => a.IdOrder);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.IdUser);
        }
    }
}
