using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item", "challenge");

            builder.HasKey(x => x.OrderItemId);

            builder.Property(x => x.OrderItemId)
                .HasColumnName("order_item_id");

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasPrecision(18, 2);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId)
                .IsRequired();

            builder.HasOne(x => x.MenuItem)
                .WithMany()
                .HasForeignKey(x => x.MenuItemId)
                .IsRequired();
        }
    }
}
