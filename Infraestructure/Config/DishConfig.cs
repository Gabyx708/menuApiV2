using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class DishConfig : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(p => p.IdDish);

            builder.HasIndex(p => p.Description).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(150);
            builder.Property(p => p.IdDish).ValueGeneratedOnAdd();
        }
    }
}
