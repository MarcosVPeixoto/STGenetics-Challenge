using AutoMapper;
using MediatR;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetAll
{
    public class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuItemsQuery, List<GetAllMenuItemsDto>>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetAllMenuItemsQueryHandler(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllMenuItemsDto>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<List<GetAllMenuItemsDto>>(menuItems);
        }
    }
}
