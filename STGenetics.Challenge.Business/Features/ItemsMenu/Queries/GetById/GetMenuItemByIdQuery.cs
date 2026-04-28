using MediatR;
using STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetById;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetById
{
    public class GetMenuItemByIdQuery : IRequest<GetMenuItemByIdDto>
    {
        public Guid MenuItemId { get; set; }

        public GetMenuItemByIdQuery(Guid menuItemId)
        {
            MenuItemId = menuItemId;
        }
    }
}
