using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class DiscountMenuItemEntityTypeConfiguration : IEntityTypeConfiguration<DiscountMenuItem>
    {
        public void Configure(EntityTypeBuilder<DiscountMenuItem> builder)
        {
            builder.ToTable("discount_menuitem", "challenge");

            builder.HasKey(x => new { x.DiscountId, x.MenuItemId });

            builder.Property(x => x.DiscountId)
                .HasColumnName("discount_id");

            builder.Property(x => x.MenuItemId)
                .HasColumnName("menu_item_id");

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.HasOne(x => x.Discount)
                .WithMany(x => x.MenuItemsRequired)
                .HasForeignKey(x => x.DiscountId)
                .IsRequired();

            builder.HasOne(x => x.MenuItem)
                .WithMany()
                .HasForeignKey(x => x.MenuItemId)
                .IsRequired();
        }
    }
}
