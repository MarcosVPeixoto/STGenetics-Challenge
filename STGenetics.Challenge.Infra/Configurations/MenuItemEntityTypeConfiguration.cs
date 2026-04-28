using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class MenuItemEntityTypeConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("menu_item", "challenge");

            builder.HasKey(x => x.MenuItemId);

            builder.Property(x => x.MenuItemId)
                .HasColumnName("menu_item_id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasPrecision(18, 2);
        }
    }
}
