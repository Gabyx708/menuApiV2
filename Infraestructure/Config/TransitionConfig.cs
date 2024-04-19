using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infraestructure.Config
{
    internal class TransitionConfig : IEntityTypeConfiguration<Transition>
    {
        public void Configure(EntityTypeBuilder<Transition> builder)
        {
            builder.HasKey(t => new { t.IdOrder, t.InitialStateCode, t.FinalStateCode });

            builder.Property(t => t.Date).IsRequired();

            builder.HasOne(t => t.InitialState)
                    .WithMany(s => s.InitialTransitions)
                    .HasForeignKey(t => t.InitialStateCode)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.FinalSate)
                    .WithMany(s => s.FinalTransitions)
                    .HasForeignKey(t => t.FinalStateCode)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
