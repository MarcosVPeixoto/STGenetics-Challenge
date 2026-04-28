using AutoMapper;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetById
{
    [AutoMap(typeof(MenuItem))]
    public class GetMenuItemByIdDto
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
