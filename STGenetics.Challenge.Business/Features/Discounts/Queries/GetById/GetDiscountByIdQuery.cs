using MediatR;

namespace STGenetics.Challenge.Business.Features.Discounts.Queries.GetById
{
    public class GetDiscountByIdQuery : IRequest<GetDiscountByIdDto>
    {
        public Guid DiscountId { get; set; }

        public GetDiscountByIdQuery(Guid discountId)
        {
            DiscountId = discountId;
        }
    }
}
