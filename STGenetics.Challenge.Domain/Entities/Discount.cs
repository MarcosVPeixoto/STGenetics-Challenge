namespace STGenetics.Challenge.Domain.Entities
{
    public class Discount
    {
        public Guid DiscountId { get; set; }        
        public string Description { get; set; }
        public bool Active { get; set; }
        public List<DiscountMenuItem> MenuItemsRequired { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}