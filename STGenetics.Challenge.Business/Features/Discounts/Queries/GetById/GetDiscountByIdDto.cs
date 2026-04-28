using AutoMapper;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Discounts.Queries.GetById
{
    [AutoMap(typeof(Discount))]
    public class GetDiscountByIdDto
    {
        public Guid DiscountId { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool Active { get; set; }
    }
}