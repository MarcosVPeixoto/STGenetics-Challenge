using Microsoft.EntityFrameworkCore;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class DiscountRepository(ApplicationDbContext context) : BaseRepository<Discount>(context), IDiscountRepository
    {
        public  async Task<List<Discount>> GetActiveDiscountsAsync()
        {
            return await _context.Discounts.Where(d => d.Active)
                                           .Include(x => x.MenuItemsRequired)
                                           .ToListAsync();
        }
    }
}
