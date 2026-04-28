using AutoMapper;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetAll
{
    [AutoMap(typeof(MenuItem))]
    public class GetAllMenuItemsDto
    {
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
