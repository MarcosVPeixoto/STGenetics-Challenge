using Microsoft.EntityFrameworkCore;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Infra.Repositories
{
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context = context;

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }   

        public async Task<T> FindAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }        

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            _context.Remove(entity);
        }
        
        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();            
        }        
    }
}
