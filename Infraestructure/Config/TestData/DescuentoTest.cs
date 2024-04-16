using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config.TestData
{
    public class DescuentoTest : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasData(

                new Discount
                {
                    IdDescuento = Guid.NewGuid(),
                    FechaInicioVigencia = DateTime.Now,
                    Porcentaje = 50
                }
            );
        }
    }
}
