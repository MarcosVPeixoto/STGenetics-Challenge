namespace STGenetics.Challenge.Business.Features.Orders.Commands.Update
{
    public class OrderItemDto
    {
        public Guid? OrderItemId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}