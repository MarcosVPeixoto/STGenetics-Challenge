using STGenetics.Challenge.Domain.Enums;

namespace STGenetics.Challenge.Domain.Entities
{
    public class MenuItem
    {
        public Guid MenuItemId { get; set; }        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MenuItemType Type { get; set; }
    }
}