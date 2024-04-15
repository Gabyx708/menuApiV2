using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class MenuPlatilloConfig : IEntityTypeConfiguration<MenuPlatillo>
    {
        public void Configure(EntityTypeBuilder<MenuPlatillo> builder)
        {
            builder.ToTable("MenuPlatillo");
            builder.HasKey(mp => mp.IdMenuPlatillo);

            builder.HasOne(mp => mp.Menu)
                .WithMany(m => m.MenuPlatillos)
                .HasForeignKey(mp => mp.IdMenu);

            builder.HasOne(mp => mp.Platillo)
                .WithMany(p => p.MenuPlatillos)
                .HasForeignKey(mp => mp.IdPlatillo);

        }
    }
}
