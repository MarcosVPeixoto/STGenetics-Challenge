using MediatR;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Delete
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
    }
}