using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;
public class GadgetConfiguration : IEntityTypeConfiguration<Gadget>
{
    public void Configure(EntityTypeBuilder<Gadget> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(g => g.Brand)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.Category)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(g => g.ReleaseDate)
            .IsRequired();

        builder.Property(g => g.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(g => g.IsAvailable)
            .IsRequired();
    }
}
