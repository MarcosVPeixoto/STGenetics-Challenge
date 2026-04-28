using AutoMapper;
using MediatR;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetById
{
    public class GetMenuItemByIdQueryHandler : IRequestHandler<GetMenuItemByIdQuery, GetMenuItemByIdDto>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetMenuItemByIdQueryHandler(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<GetMenuItemByIdDto> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.FindAsync(request.MenuItemId);
            return _mapper.Map<GetMenuItemByIdDto>(menuItem);
        }
    }
}
