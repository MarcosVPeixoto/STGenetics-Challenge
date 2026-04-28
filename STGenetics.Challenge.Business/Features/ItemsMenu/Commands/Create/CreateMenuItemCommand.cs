using MediatR;
using STGenetics.Challenge.Domain.Enums;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Commands.Create
{
    public class CreateMenuItemCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MenuItemType Type { get; set; }
    }
}
