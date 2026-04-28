using MediatR;


namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQuery : IRequest<List<GetAllOrderDto>>
    {
    }
}
