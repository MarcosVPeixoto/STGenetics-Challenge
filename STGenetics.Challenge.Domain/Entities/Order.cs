using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Challenge.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Discount Discount { get; set; }
        public decimal Total { get; set; }

        public void GetBestDiscount(List<Discount> activeDiscounts)
        {
            var bestDiscount = activeDiscounts
                .Where(d => d.MenuItemsRequired.All(mi => OrderItems.Any(oi => oi.MenuItemId == mi.MenuItemId)))
                .OrderByDescending(d => d.DiscountPercentage)
                .FirstOrDefault();

            if (bestDiscount != null)
            {
                Discount = bestDiscount;
                Total = Math.Round(OrderItems.Sum(oi => oi.Price * oi.Quantity) * (1 - bestDiscount.DiscountPercentage / 100), 2);
            }
            else
            {
                Total = OrderItems.Sum(oi => oi.Price * oi.Quantity);
            }
        }
        public void AddItems(List<OrderItem> newItems)
        {
            OrderItems.AddRange(newItems);
        }

        public void RemoveItems(List<OrderItem> removedItems)
        {
            foreach (var item in removedItems)
            {
                OrderItems.Remove(item);
            }
        }
    }
}