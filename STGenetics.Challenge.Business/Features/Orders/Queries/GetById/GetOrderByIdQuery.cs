using MediatR;

namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<GetOrderByIdDto>
    {
        public Guid OrderId { get; set; }

        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
