namespace STGenetics.Challenge.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public Guid MenuItemId { get; set; }
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public decimal Price { get; set; }   
        public int Quantity { get; set; }     
    }
}