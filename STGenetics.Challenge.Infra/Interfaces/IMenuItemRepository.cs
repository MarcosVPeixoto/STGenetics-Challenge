using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Infra.Interfaces
{
    public interface IMenuItemRepository : IBaseRepository<MenuItem>
    {
        Task<bool> ExistAllAsync(List<Guid> ids);
        Task<List<MenuItem>> GetByIds(List<Guid> ids);
    }
}
