using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Infra.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task AddItemAsync(OrderItem orderItem);
        Task<bool> ExistAllOrderItemsAsync(List<Guid> ids);
        Task<Order> GetById(Guid id);
        void RemoveItem(OrderItem item);
    }
}
