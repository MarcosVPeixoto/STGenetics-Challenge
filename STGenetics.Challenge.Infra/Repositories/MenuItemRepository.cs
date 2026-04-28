using Microsoft.EntityFrameworkCore;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class MenuItemRepository(ApplicationDbContext context) : BaseRepository<MenuItem>(context), IMenuItemRepository
    {
        public async Task<bool> ExistAllAsync(List<Guid> ids)
        {
            return await _context.MenuItems.Where(mi => ids.Contains(mi.MenuItemId)).CountAsync() == ids.Count;
        }

        public async Task<List<MenuItem>> GetByIds(List<Guid> ids)
        {
            return await _context.MenuItems.Where(mi => ids.Contains(mi.MenuItemId)).ToListAsync();
        }
    }
}
