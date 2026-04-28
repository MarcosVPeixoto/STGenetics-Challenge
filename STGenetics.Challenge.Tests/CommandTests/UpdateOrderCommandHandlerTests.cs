using AutoMapper;
using Moq;
using STGenetics.Challenge.Business.Features.Orders.Commands.Update;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Domain.Enums;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Tests.CommandTests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
        private readonly Mock<IDiscountRepository> _discountRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly List<Discount> _activeDiscounts;
        private MenuItem _xBurguer = new() { MenuItemId = Guid.NewGuid(), Name = "X burguer", Price = 5, Type = MenuItemType.Sandwich };
        private MenuItem _xEgg = new() { MenuItemId = Guid.NewGuid(), Name = "X Egg", Price = 4.5m, Type = MenuItemType.Sandwich };
        private MenuItem _xBacon = new() { MenuItemId = Guid.NewGuid(), Name = "X Bacon", Price = 7, Type = MenuItemType.Sandwich };
        private MenuItem _batataFrita = new() { MenuItemId = Guid.NewGuid(), Name = "Batata Frita", Price = 2, Type = MenuItemType.Accompaniment };
        private MenuItem _refrigerante = new() { MenuItemId = Guid.NewGuid(), Name = "Refrigerante", Price = 2.5m, Type = MenuItemType.SoftDrink };
        public UpdateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _menuItemRepositoryMock = new Mock<IMenuItemRepository>();
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _mapperMock = new Mock<IMapper>();
            _activeDiscounts = CreateDiscounts();
            _discountRepositoryMock.Setup(repo => repo.GetActiveDiscountsAsync())
                .ReturnsAsync(_activeDiscounts);
            _menuItemRepositoryMock.Setup(repo => repo.GetByIds(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<MenuItem>() { _xBurguer, _xEgg, _xBacon, _batataFrita, _refrigerante });
        }

        private List<Discount> CreateDiscounts()
        {
            var result = new List<Discount>();
            result.AddRange(Create20PercentDiscounts());
            result.AddRange(Create15PercentDiscounts());
            result.AddRange(Create10PercentDiscounts());
            return result;
        }

        private List<Discount> Create20PercentDiscounts()
        {
            var result = new List<Discount>();
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata + refrigerante → 20% de desconto",
                Active = true,
                DiscountPercentage = 20,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata + refrigerante → 20% de desconto",
                Active = true,
                DiscountPercentage = 20,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata + refrigerante → 20% de desconto",
                Active = true,
                DiscountPercentage = 20,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            return result;
        }

        private List<Discount> Create15PercentDiscounts()
        {
            var result = new List<Discount>();
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + refrigerante → 15% de desconto",
                Active = true,
                DiscountPercentage = 15,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBacon.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + refrigerante → 15% de desconto",
                Active = true,
                DiscountPercentage = 15,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + refrigerante → 15% de desconto",
                Active = true,
                DiscountPercentage = 15,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            });
            return result;
        }

        private List<Discount> Create10PercentDiscounts()
        {
            var result = new List<Discount>();
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata → 10% de desconto",
                Active = true,
                DiscountPercentage = 10,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBacon.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata → 10% de desconto",
                Active = true,
                DiscountPercentage = 10,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 }
                }
            });
            result.Add(new Discount()
            {
                DiscountId = Guid.NewGuid(),
                Description = "Sanduíche + batata → 10% de desconto",
                Active = true,
                DiscountPercentage = 10,
                MenuItemsRequired = new List<DiscountMenuItem>()
                {
                    new DiscountMenuItem() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new DiscountMenuItem() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 }
                }
            });
            return result;
        }

        [Fact]
        public async Task Handle_ShouldUpdateOrder_WhenCommandIsValid()
        {
            // Arrange
            var xburguer = new OrderItem() { OrderItemId = Guid.NewGuid(), MenuItemId = _xBurguer.MenuItemId, Quantity = 1, Price = _xBurguer.Price };
            var batataFrita = new OrderItem() { OrderItemId = Guid.NewGuid(), MenuItemId = _batataFrita.MenuItemId, Quantity = 1, Price = _batataFrita.Price };
            var refrigerante = new OrderItem() { OrderItemId = Guid.NewGuid(), MenuItemId = _refrigerante.MenuItemId, Quantity = 1, Price = _refrigerante.Price };
            var order = new Order()
            {
                OrderId = Guid.NewGuid(),
                OrderItems = new List<OrderItem>()
                {
                    xburguer, batataFrita, refrigerante
                }
            };
            _orderRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(order);
            _menuItemRepositoryMock.Setup(repo => repo.GetByIds(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<MenuItem>() { _xBurguer, _xEgg, _xBacon, _batataFrita, _refrigerante });

            var handler = new UpdateOrderCommandHandler(_orderRepositoryMock.Object, _discountRepositoryMock.Object, _menuItemRepositoryMock.Object);
            var command = new UpdateOrderCommand()
            {
                OrderId = order.OrderId,
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 },                    
                    new OrderItemDto() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert      
            Assert.Equal(5.95m,result.Total);
            Assert.Equal(2, order.OrderItems.Count);
        }
    }
}