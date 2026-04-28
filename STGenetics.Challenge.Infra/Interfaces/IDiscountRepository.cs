using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Infra.Interfaces
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<List<Discount>> GetActiveDiscountsAsync();
    }
}
