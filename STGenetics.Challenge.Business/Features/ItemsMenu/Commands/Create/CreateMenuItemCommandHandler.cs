using MediatR;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Commands.Create
{
    public class CreateMenuItemCommandHandler : IRequestHandler<CreateMenuItemCommand, Guid>
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public CreateMenuItemCommandHandler(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<Guid> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItemId = Guid.NewGuid();
            var menuItem = new MenuItem
            {
                MenuItemId = menuItemId,
                Name = request.Name,
                Price = request.Price,
                Type = request.Type
            };

            await _menuItemRepository.AddAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
            return menuItem.MenuItemId;
        }
    }
}
