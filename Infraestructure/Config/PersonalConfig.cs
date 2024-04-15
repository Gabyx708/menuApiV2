using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class PersonalConfig : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> entityBuilder)
        {
            entityBuilder.ToTable("Personal");
            entityBuilder.HasKey(p => p.IdPersonal);

            entityBuilder.Property(p => p.Nombre).HasMaxLength(50).IsRequired();
            entityBuilder.Property(p => p.Apellido).HasMaxLength(20).IsRequired();
            entityBuilder.Property(p => p.Dni).HasMaxLength(15).IsRequired();
            entityBuilder.Property(p => p.Mail).HasMaxLength(100);
            entityBuilder.Property(p => p.Telefono).HasMaxLength(30);
            entityBuilder.Property(p => p.Privilegio).HasMaxLength(2).IsRequired();
            entityBuilder.Property(p => p.Password).IsRequired();


            //relacion
            entityBuilder.HasMany(p => p.Pedidos)
                .WithOne(pe => pe.Personal)
                .HasForeignKey(pe => pe.IdPersonal);
        }
    }
}
