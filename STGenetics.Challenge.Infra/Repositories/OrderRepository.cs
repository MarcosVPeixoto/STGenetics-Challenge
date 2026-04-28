using Microsoft.EntityFrameworkCore;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task AddItemAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }

        public async Task<bool> ExistAllOrderItemsAsync(List<Guid> ids)
        {
            return await _context.OrderItems.Where(oi => ids.Contains(oi.OrderItemId)).CountAsync() == ids.Count;
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .ThenInclude(x => x.MenuItem)
                                        .Include(x => x.Discount)
                                        .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public void RemoveItem(OrderItem item)
        {
            _context.OrderItems.Remove(item);
        }
    }
}
