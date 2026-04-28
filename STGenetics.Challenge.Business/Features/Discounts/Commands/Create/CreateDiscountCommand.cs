using MediatR;

namespace STGenetics.Challenge.Business.Features.Discounts.Commands.Create
{
    public class CreateDiscountCommand : IRequest<Guid>
    {
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public List<DiscountMenuItemDto> MenuItemsRequired { get; set; }
    }
}
