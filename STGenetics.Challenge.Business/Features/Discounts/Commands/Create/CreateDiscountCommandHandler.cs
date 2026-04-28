using MediatR;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Discounts.Commands.Create
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Guid>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMenuItemRepository menuItemRepository)
        {
            _discountRepository = discountRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var menuItems = await _menuItemRepository.GetByIds(request.MenuItemsRequired.Select(m => m.MenuItemId).ToList());
            
            var discountId = Guid.NewGuid();
            var discount = new Discount
            {
                DiscountId = discountId,
                Description = request.Description,
                DiscountPercentage = request.DiscountPercentage,
                Active = true,
                MenuItemsRequired = request.MenuItemsRequired.Select(mi => new DiscountMenuItem
                {
                    DiscountId = discountId,
                    MenuItemId = mi.MenuItemId,
                    Quantity = mi.Quantity,
                    MenuItem = menuItems.First(m => m.MenuItemId == mi.MenuItemId)
                }).ToList()
            };

            await _discountRepository.AddAsync(discount);
            await _discountRepository.SaveChangesAsync();
            return discount.DiscountId;
        }
    }
}
