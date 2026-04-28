using System.Collections.Generic;
using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("discount", "challenge");

            builder.HasKey(x => x.DiscountId);

            builder.Property(x => x.DiscountId)
                .HasColumnName("discount_id");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(200);

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("active");

            builder.Property(x => x.DiscountPercentage)
                .IsRequired()
                .HasColumnName("discount_percentage")
                .HasPrecision(18, 2);

            builder.HasMany(x => x.MenuItemsRequired)
                .WithOne(x => x.Discount)
                .HasForeignKey(x => x.DiscountId)
                .IsRequired();
            
        }
    }
}
