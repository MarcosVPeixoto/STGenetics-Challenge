using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
                
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByUserId(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
