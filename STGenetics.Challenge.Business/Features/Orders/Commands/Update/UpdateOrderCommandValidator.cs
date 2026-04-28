using FluentValidation;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Update
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator(IMenuItemRepository menuItemRepository, IOrderRepository orderRepository)
        {
            RuleFor(x => x.OrderItems)
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
                        context.AddFailure("OrderItems", "O pedido não pode conter mais de um item do mesmo tipo");
                    }
                });

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("O pedido deve conter pelo menos um item do cardápio")
                .MustAsync(async (menuItems, cancellation) =>
                {
                    var ids = menuItems.Select(m => m.MenuItemId).ToList();
                    return await menuItemRepository.ExistAllAsync(ids);
                })
                .WithMessage("Um ou mais itens do cardápio são inválidos");

            RuleForEach(x => x.OrderItems)
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

            RuleFor(x => x.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido")
                .MustAsync(async (id, cancellation) =>
                {
                    var order = await orderRepository.GetById(id);
                    return order != null;
                })
                .WithMessage("Pedido não encontrado");

            RuleFor(x => x.OrderItems)
                .CustomAsync(async (menuItems, context, token) =>
                {
                    var ids = menuItems.Where(x => x.OrderItemId != null)
                                       .Select(x => x.OrderItemId.GetValueOrDefault())
                                       .ToList();
                    var validIds = await orderRepository.ExistAllOrderItemsAsync(ids);
                    if (!validIds)
                    {
                        context.AddFailure("OrderItems", "Um ou mais itens do pedido são inválidos");
                    }
                });
        }
    }
}