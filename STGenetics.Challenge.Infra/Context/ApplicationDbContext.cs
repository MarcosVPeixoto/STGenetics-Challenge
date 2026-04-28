using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace STGenetics.Challenge.Infra.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountMenuItem> DiscountMenuItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserEntityTypeConfiguration()); 
            builder.ApplyConfiguration(new DiscountEntityTypeConfiguration());
            builder.ApplyConfiguration(new DiscountMenuItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new MenuItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            builder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        }
    }
}
