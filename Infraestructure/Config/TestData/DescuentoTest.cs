using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config.TestData
{
    public class DescuentoTest : IEntityTypeConfiguration<Descuento>
    {
        public void Configure(EntityTypeBuilder<Descuento> builder)
        {
            builder.HasData(

                new Descuento
                {
                    IdDescuento = Guid.NewGuid(),
                    FechaInicioVigencia = DateTime.Now,
                    Porcentaje = 50
                }
            );
        }
    }
}
