using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class ReceiptConfig : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasKey(r => r.IdReceipt);

            builder.HasOne(r => r.Order)
                    .WithOne(o => o.Receipt)
                    .HasForeignKey<Receipt>(r => r.IdOrder);


        }
    }
}
