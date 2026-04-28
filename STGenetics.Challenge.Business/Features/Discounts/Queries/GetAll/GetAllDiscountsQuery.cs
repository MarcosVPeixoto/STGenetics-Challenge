using MediatR;

namespace STGenetics.Challenge.Business.Features.Discounts.Queries.GetAll
{
    public class GetAllDiscountsQuery : IRequest<List<GetAllDiscountDto>>
    {
    }
}
