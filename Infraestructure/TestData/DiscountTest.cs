using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.TestData
{
    public class DiscountTest : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasData(

                new Discount
                {
                    IdDiscount = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    Percentage = 50
                }
            );
        }
    }
}
