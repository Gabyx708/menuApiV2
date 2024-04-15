using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class PlatilloConfig : IEntityTypeConfiguration<Platillo>
    {
        public void Configure(EntityTypeBuilder<Platillo> builder)
        {
            builder.ToTable("Platillo");
            builder.HasKey(p => p.IdPlatillo);

            builder.Property(p => p.IdPlatillo).ValueGeneratedOnAdd();
        }
    }
}
