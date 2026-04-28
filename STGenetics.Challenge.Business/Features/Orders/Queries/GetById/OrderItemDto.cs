using AutoMapper;
using AutoMapper.Configuration.Annotations;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetById
{
    [AutoMap(typeof(OrderItem))]
    public class OrderItemDto
    {
        public Guid OrderItemId { get; set; }
        public Guid MenuItemId { get; set; }
        [SourceMember("MenuItem.Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}