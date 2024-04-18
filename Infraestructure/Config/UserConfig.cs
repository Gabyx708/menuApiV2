using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(u => u.IdUser);

            entityBuilder.Property(u => u.Name).HasMaxLength(50).IsRequired();
            entityBuilder.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            entityBuilder.Property(a => a.BirthDate).IsRequired();
            entityBuilder.Property(u => u.Password).IsRequired();


            entityBuilder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.IdUser);
        }
    }
}
