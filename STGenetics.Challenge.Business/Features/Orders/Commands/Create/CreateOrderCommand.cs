using MediatR;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest<CreateOrderDto>
    {
        public List<MenuItemDto> MenuItems { get; set; }
    }
}
