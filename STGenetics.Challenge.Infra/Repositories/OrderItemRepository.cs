using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class OrderItemRepository(ApplicationDbContext context) : BaseRepository<OrderItem>(context), IOrderItemRepository
    {
    }
}
