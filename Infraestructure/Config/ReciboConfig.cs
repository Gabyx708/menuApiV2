﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    public class ReciboConfig : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.ToTable("Recibo");
            builder.HasKey(r => r.IdRecibo);

            builder.HasMany(r => r.Pedidos)
                .WithOne(p => p.Recibo)
                .HasForeignKey(p => p.IdRecibo)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
