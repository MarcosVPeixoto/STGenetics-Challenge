using FluentValidation;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Discounts.Commands.Create
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator(IMenuItemRepository menuItemRepository)
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("A descrição do desconto é obrigatória")
                .MaximumLength(255)
                .WithMessage("A descrição do desconto não pode exceder 255 caracteres");

            RuleFor(x => x.DiscountPercentage)
                .GreaterThan(0)
                .WithMessage("O percentual de desconto deve ser maior que 0")
                .LessThanOrEqualTo(100)
                .WithMessage("O percentual de desconto não pode exceder 100%");

            RuleFor(x => x.MenuItemsRequired)
                .NotEmpty()
                .WithMessage("O desconto deve conter pelo menos um item do cardápio")
                .Custom((menuItems, context) =>
                {
                    if (menuItems == null) return;

                    var duplicateMenuItemIds = menuItems
                        .GroupBy(m => m.MenuItemId)
                        .Where(g => g.Count() > 1)
                        .Select(g => g.Key)
                        .ToList();

                    if (duplicateMenuItemIds.Any())
                    {
                        context.AddFailure("MenuItemsRequired", "Não é permitido repetir o mesmo item do cardápio no desconto");
                    }
                })
                .MustAsync(async (menuItems, cancellation) =>
                {
                    var ids = menuItems.Select(m => m.MenuItemId).ToList();
                    return await menuItemRepository.ExistAllAsync(ids);
                })
                .WithMessage("Um ou mais itens do cardápio são inválidos");

            RuleForEach(x => x.MenuItemsRequired)
                .ChildRules(menuItem =>
                {
                    menuItem.RuleFor(m => m.MenuItemId)
                        .NotEqual(Guid.Empty)
                        .WithMessage("Item do cardápio inválido");

                    menuItem.RuleFor(m => m.Quantity)
                        .GreaterThan(0)
                        .WithMessage("A quantidade deve ser maior que 0");
                });
        }
    }
}
