using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Infra.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUserId(Guid userId);
    }
}
