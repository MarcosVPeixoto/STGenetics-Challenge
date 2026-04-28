using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Challenge.Domain.Entities
{
    public class DiscountMenuItem
    {
        public Guid DiscountId { get; set; }
        public Discount Discount { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }
    }
}