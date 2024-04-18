using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Config
{
    internal class TransitionConfig : IEntityTypeConfiguration<Transition>
    {
        public void Configure(EntityTypeBuilder<Transition> builder)
        {
            builder.HasKey(t => new { t.IdOrder, t.InitialStateCode, t.FinalStateCode });

            builder.Property(t => t.Date).IsRequired();
        }
    }
}
