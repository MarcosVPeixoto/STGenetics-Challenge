using MediatR;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetAll
{
    public class GetAllMenuItemsQuery : IRequest<List<GetAllMenuItemsDto>>
    {
    }
}
