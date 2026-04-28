using AutoMapper;
using MediatR;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Update
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IDiscountRepository discountRepository, IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _discountRepository = discountRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<UpdateOrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById(request.OrderId);
            if (order == null)
            {
                throw new Exception("Pedido não encontrado");
            }
            var newItems = await CreateNewItems(request, order);
            var removedItems = RemoveItems(request, order);            
            order.RemoveItems(removedItems);
            order.AddItems(newItems);
            var discounts = await _discountRepository.GetActiveDiscountsAsync();
            order.GetBestDiscount(discounts);            
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
            return new UpdateOrderDto()
            {
                Total = order.Total,
                OrderId = order.OrderId
            };  
        }

        private async Task<List<OrderItem>> CreateNewItems(UpdateOrderCommand request, Order order)
        {
            var newItems = request.OrderItems.Where(oi => oi.OrderItemId == null).ToList();
            var result = new List<OrderItem>();
            var menuItems = await _menuItemRepository.GetByIds(newItems.Select(ni => ni.MenuItemId).ToList());
            foreach (var item in newItems)
            {
                var menuItem = menuItems.First(mi => mi.MenuItemId == item.MenuItemId);
                var orderItem = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    OrderId = order.OrderId,
                    Price = menuItem.Price
                };
                result.Add(orderItem);
                await _orderRepository.AddItemAsync(orderItem);
            }
            return result;
        }

        private List<OrderItem> RemoveItems(UpdateOrderCommand request, Order order)
        {
            var requestIds = request.OrderItems.Select(oi => oi.OrderItemId).ToList();
            var itemsToRemove = order.OrderItems.Where(oi => !requestIds.Contains(oi.OrderItemId)).ToList();
            return itemsToRemove;
        }
    }
}