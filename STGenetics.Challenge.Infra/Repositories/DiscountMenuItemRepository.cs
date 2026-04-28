using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class DiscountMenuItemRepository(ApplicationDbContext context) : BaseRepository<DiscountMenuItem>(context), IDiscountMenuItemRepository
    {
    }
}
