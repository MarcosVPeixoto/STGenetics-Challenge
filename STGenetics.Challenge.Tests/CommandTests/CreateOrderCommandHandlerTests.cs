using AutoMapper;
using Moq;
using STGenetics.Challenge.Business.Features.Orders.Commands.Create;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Domain.Enums;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Tests.CommandTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
        private readonly Mock<IDiscountRepository> _discountRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly List<Discount> _activeDiscounts;
        private MenuItem _xBurguer = new (){ MenuItemId = Guid.NewGuid(), Name = "X burguer", Price = 5, Type = MenuItemType.Sandwich};
        private MenuItem _xEgg = new (){ MenuItemId = Guid.NewGuid(), Name = "X Egg", Price = 4.5m, Type = MenuItemType.Sandwich};
        private MenuItem _xBacon = new (){ MenuItemId = Guid.NewGuid(), Name = "X Bacon", Price = 7, Type = MenuItemType.Sandwich};
        private MenuItem _batataFrita = new (){ MenuItemId = Guid.NewGuid(), Name = "Batata Frita", Price = 2, Type = MenuItemType.Accompaniment};
        private MenuItem _refrigerante = new (){ MenuItemId = Guid.NewGuid(), Name = "Refrigerante", Price = 2.5m, Type = MenuItemType.SoftDrink};
        public CreateOrderCommandHandlerTests()
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
        public async Task Handle_ShouldGive20PercentDiscount_FullMeal()
        {
            // Arrange
            var command = new CreateOrderCommand()
            {
                MenuItems = new ()
                {
                    new MenuItemDto() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new MenuItemDto() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 },
                    new MenuItemDto() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            };
            var handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _discountRepositoryMock.Object, _menuItemRepositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert            
            Assert.Equal(7.6m, result.Total); 
        }

        [Fact]
        public async Task Handle_ShouldGive15PercentDiscount_SandwichAndSoftDrink()
        {
            // Arrange
            var command = new CreateOrderCommand()
            {
                MenuItems = new ()
                {
                    new MenuItemDto() { MenuItemId = _xBacon.MenuItemId, Quantity = 1 },
                    new MenuItemDto() { MenuItemId = _refrigerante.MenuItemId, Quantity = 1 }
                }
            };
            var handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _discountRepositoryMock.Object, _menuItemRepositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert            
            Assert.Equal(8.08m, result.Total);
        }

        [Fact]
        public async Task Handle_ShouldGive10PercentDiscount_SandwichAndAccompaniment()
        {
            // Arrange
            var command = new CreateOrderCommand()
            {
                MenuItems = new ()
                {
                    new MenuItemDto() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 },
                    new MenuItemDto() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 }
                }
            };
            var handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _discountRepositoryMock.Object, _menuItemRepositoryMock.Object, _mapperMock.Object);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert            
            Assert.Equal(5.85m, result.Total);
        }
    }
}