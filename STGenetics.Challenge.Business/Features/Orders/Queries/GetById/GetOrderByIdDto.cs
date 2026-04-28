using AutoMapper;
using AutoMapper.Configuration.Annotations;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetById
{
    [AutoMap(typeof(Order))]
    public class GetOrderByIdDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal => OrderItems.Sum(oi => oi.Price * oi.Quantity);
        public List<OrderItemDto> OrderItems { get; set; }
        [SourceMember("Discount.DiscountPercentage")]
        public int DiscountPercentage { get; set; }
    }
}
