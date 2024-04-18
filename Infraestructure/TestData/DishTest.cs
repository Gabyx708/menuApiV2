using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.TestData
{
    public class DishTest : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasData(

                new Dish
                {
                    IdDish = 1,
                    Description = "Ravioli de ricotta y espinacas con salsa de tomate",
                    Price = 1000,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 2,
                    Description = "milanesa a la napolitana",
                    Price = 3000,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 3,
                    Description = "Ceviche de camarón y pescado",
                    Price = 2800,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 4,
                    Description = "Costillas de cerdo a la barbacoa con salsa ahumada",
                    Price = 357,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 5,
                    Description = "Paella mixta de mariscos y pollo",
                    Price = 1890,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 6,
                    Description = "Salmón con verduras salteadas y arroz jazmín",
                    Price = 100,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 7,
                    Description = "Lasaña de carne y verduras con capas de pasta",
                    Price = 1200,
                    Activated = true
                },
                new Dish
                {
                    IdDish = 8,
                    Description = "Pechuga de pollo rellena de queso de cabra ",
                    Price = 1500,
                    Activated = true
                }


            );
        }
    }
}
