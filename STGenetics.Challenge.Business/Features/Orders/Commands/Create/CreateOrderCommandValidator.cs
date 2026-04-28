using FluentValidation;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Create
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(IMenuItemRepository menuItemRepository)
        {
            RuleFor(x => x.MenuItems)
                .CustomAsync(async (menuItems, context, token) =>
                {
                    if (menuItems == null) return;
                    var items = await menuItemRepository.GetByIds(menuItems.Select(m => m.MenuItemId).ToList());
                    var menuItemsIds = menuItems.Select(m => m.MenuItemId).ToList();
                    var types = items.Where(x => menuItemsIds.Contains(x.MenuItemId))
                                     .GroupBy(x => x.Type)
                                     .Where(g => g.Count() > 1)
                                     .Select(g => g.Key)
                                     .ToList();
                    if (types.Any())
                    {
                        context.AddFailure("MenuItems", "O pedido não pode conter mais de um item do mesmo tipo");
                    }
                    
                });
            
            RuleFor(x => x.MenuItems)
                .NotEmpty()
                .WithMessage("O pedido deve conter pelo menos um item do cardápio")
                .MustAsync(async (menuItems, cancellation) =>
                {
                   var ids = menuItems.Select(m => m.MenuItemId).ToList();
                   return await menuItemRepository.ExistAllAsync(ids);
                })
                .WithMessage("Um ou mais itens do cardápio são inválidos");

            RuleForEach(x => x.MenuItems)
                .ChildRules(menuItem =>
                {
                    menuItem.RuleFor(m => m.MenuItemId)
                        .NotEqual(Guid.Empty)
                        .WithMessage("Item do cardápio inválido");

                    menuItem.RuleFor(m => m.Quantity)
                        .LessThanOrEqualTo(1)
                        .WithMessage("O máximo de itens do pedido é 1")
                        .GreaterThan(0)
                        .WithMessage("A quantidade deve ser maior que 0");
                        
                });
        }
    }
}
