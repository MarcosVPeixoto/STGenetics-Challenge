using AutoMapper;
using MediatR;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IDiscountRepository discountRepository, IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _discountRepository = discountRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<CreateOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var menuItems = await _menuItemRepository.GetByIds(request.MenuItems.Select(m => m.MenuItemId).ToList());
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                OrderId = orderId,
                OrderItems = request.MenuItems.Select(mi => new OrderItem
                {
                    OrderId = orderId,
                    OrderItemId = Guid.NewGuid(),
                    MenuItemId = mi.MenuItemId,
                    Quantity = mi.Quantity,
                    Price = menuItems.First(m => m.MenuItemId == mi.MenuItemId).Price
                }).ToList()
            };
            var activeDiscounts = await _discountRepository.GetActiveDiscountsAsync();
            order.GetBestDiscount(activeDiscounts);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
            return new CreateOrderDto
            {
                OrderId = order.OrderId,
                Total = order.Total
            };
            
        }
    }
}
