using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order", "challenge");

            builder.HasKey(x => x.OrderId);

            builder.Property(x => x.OrderId)
                .HasColumnName("order_id");

            builder.Property(x => x.Total)
                .IsRequired()
                .HasColumnName("total")
                .HasPrecision(18, 2);

            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired();
        }
    }
}
