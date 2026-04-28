using STGenetics.Challenge.Infra.Interfaces;
using STGenetics.Challenge.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace STGenetics.Challenge.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDiscountMenuItemRepository, DiscountMenuItemRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
