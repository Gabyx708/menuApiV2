using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class MenuOptionConfig : IEntityTypeConfiguration<MenuOption>
    {
        public void Configure(EntityTypeBuilder<MenuOption> builder)
        {
            builder.HasKey(mo => new { mo.IdMenu, mo.IdDish });

            builder.HasOne(mo => mo.Menu)
                .WithMany(m => m.Options)
                .HasForeignKey(mo => mo.IdMenu);

            builder.HasOne(mo => mo.Dish)
                .WithMany()
                .HasForeignKey(mo => mo.IdDish)
                .HasPrincipalKey(d => d.IdDish);

        }
    }
}
