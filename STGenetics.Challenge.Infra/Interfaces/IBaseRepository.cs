namespace STGenetics.Challenge.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> FindAsync(Guid id);
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task SaveChangesAsync();
    }
}
