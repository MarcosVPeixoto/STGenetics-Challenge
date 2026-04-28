using FluentAssertions;
using FluentValidation;
using Moq;
using STGenetics.Challenge.Business.Features.Orders.Commands.Update;
using STGenetics.Challenge.Domain.Entities;
using STGenetics.Challenge.Domain.Enums;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Tests.Validators
{
    public class UpdateOrderCommandValidatorTests : AbstractValidator<UpdateOrderCommandValidator>
    {
        private readonly Mock<IMenuItemRepository> _menuItemRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private MenuItem _xBurguer = new() { MenuItemId = Guid.NewGuid(), Name = "X burguer", Price = 5, Type = MenuItemType.Sandwich };
        private MenuItem _xEgg = new() { MenuItemId = Guid.NewGuid(), Name = "X Egg", Price = 4.5m, Type = MenuItemType.Sandwich };
        private MenuItem _xBacon = new() { MenuItemId = Guid.NewGuid(), Name = "X Bacon", Price = 7, Type = MenuItemType.Sandwich };
        private MenuItem _batataFrita = new() { MenuItemId = Guid.NewGuid(), Name = "Batata Frita", Price = 2, Type = MenuItemType.Accompaniment };
        private MenuItem _refrigerante = new() { MenuItemId = Guid.NewGuid(), Name = "Refrigerante", Price = 2.5m, Type = MenuItemType.SoftDrink };

        public UpdateOrderCommandValidatorTests()
        {
            _menuItemRepositoryMock = new Mock<IMenuItemRepository>();
            _menuItemRepositoryMock.Setup(r => r.GetByIds(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<MenuItem> { _xBurguer, _xEgg, _xBacon, _batataFrita, _refrigerante });
            _orderRepositoryMock = new Mock<IOrderRepository>();
        }



        [Fact]
        public async Task Validate_ShouldThrowError_WhenRepeatedTypes()
        {
            // Arrange
            var validator = new UpdateOrderCommandValidator(_menuItemRepositoryMock.Object, _orderRepositoryMock.Object);
            _menuItemRepositoryMock.Setup(x => x.ExistAllAsync(It.IsAny<List<Guid>>()))
                                   .ReturnsAsync(true);
            var command = new UpdateOrderCommand()
            {
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new OrderItemDto() { MenuItemId = _xEgg.MenuItemId, Quantity = 1 }
                }
            };
            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "OrderItems" && e.ErrorMessage == "O pedido não pode conter mais de um item do mesmo tipo");
        }

        [Fact]
        public async Task Validate_ShouldThrowError_WhenMeuItemNotFound()
        {
            // Arrange
            var validator = new UpdateOrderCommandValidator(_menuItemRepositoryMock.Object, _orderRepositoryMock.Object);
            _menuItemRepositoryMock.Setup(x => x.ExistAllAsync(It.IsAny<List<Guid>>()))
                                   .ReturnsAsync(false);
            var command = new UpdateOrderCommand()
            {
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto() { MenuItemId = Guid.NewGuid(), Quantity = 1 }
                }
            };
            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "OrderItems" && e.ErrorMessage == "Um ou mais itens do cardápio são inválidos");
        }

        [Fact]
        public async Task Validate_ShouldThrowError_WhenQuantityInvalid()
        {
            // Arrange
            var validator = new UpdateOrderCommandValidator(_menuItemRepositoryMock.Object, _orderRepositoryMock.Object);
            _menuItemRepositoryMock.Setup(x => x.ExistAllAsync(It.IsAny<List<Guid>>()))
                                   .ReturnsAsync(true);
            var command = new UpdateOrderCommand()
            {
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto() { MenuItemId = _xBurguer.MenuItemId, Quantity = 2 }
                }
            };
            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "OrderItems[0].Quantity" && e.ErrorMessage == "O máximo de itens do pedido é 1");
        }

        [Fact]
        public async Task Validate_ShouldPass_WhenValidCommand()
        {
            // Arrange
            var validator = new UpdateOrderCommandValidator(_menuItemRepositoryMock.Object, _orderRepositoryMock.Object);
            _menuItemRepositoryMock.Setup(x => x.ExistAllAsync(It.IsAny<List<Guid>>()))
                                   .ReturnsAsync(true);
            _orderRepositoryMock.Setup(x => x.ExistAllOrderItemsAsync(It.IsAny<List<Guid>>()))
                                .ReturnsAsync(true);
            _orderRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>()))
                                .ReturnsAsync(new Order() { OrderId = Guid.NewGuid() });
            var command = new UpdateOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                OrderItems = new List<OrderItemDto>()
                {
                    new OrderItemDto() { MenuItemId = _xBurguer.MenuItemId, Quantity = 1 },
                    new OrderItemDto() { MenuItemId = _batataFrita.MenuItemId, Quantity = 1 }
                }
            };
            // Act
            var result = await validator.ValidateAsync(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}