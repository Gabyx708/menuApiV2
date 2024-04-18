using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.IdMenu);

            builder.Property(m => m.UploadDate).IsRequired();
            builder.Property(m => m.CloseDate).IsRequired();
            builder.Property(m => m.EatingDate).IsRequired();
                         
        }
    }
}
