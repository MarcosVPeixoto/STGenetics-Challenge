namespace STGenetics.Challenge.Business.Features.Orders.Commands.Update
{
    public class UpdateOrderDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}