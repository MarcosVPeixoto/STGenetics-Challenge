using STGenetics.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace STGenetics.Challenge.Infra.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "challenge");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("user_id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(50);

        }
    }
}
