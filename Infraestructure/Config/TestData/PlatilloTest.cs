using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config.TestData
{
    public class PlatilloTest : IEntityTypeConfiguration<Platillo>
    {
        public void Configure(EntityTypeBuilder<Platillo> builder)
        {
            builder.HasData(

                new Platillo
                {
                    IdPlatillo = 1,
                    Descripcion = "Ravioli de ricotta y espinacas con salsa de tomate",
                    Precio = 1000,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 2,
                    Descripcion = "milanesa a la napolitana",
                    Precio = 3000,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 3,
                    Descripcion = "Ceviche de camarón y pescado",
                    Precio = 2800,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 4,
                    Descripcion = "Costillas de cerdo a la barbacoa con salsa ahumada",
                    Precio = 357,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 5,
                    Descripcion = "Paella mixta de mariscos y pollo",
                    Precio = 1890,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 6,
                    Descripcion = "Salmón con verduras salteadas y arroz jazmín",
                    Precio = 100,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 7,
                    Descripcion = "Lasaña de carne y verduras con capas de pasta",
                    Precio = 1200,
                    Activado = true
                },
                new Platillo
                {
                    IdPlatillo = 8,
                    Descripcion = "Pechuga de pollo rellena de queso de cabra ",
                    Precio = 1500,
                    Activado = true
                }


            );
        }
    }
}
