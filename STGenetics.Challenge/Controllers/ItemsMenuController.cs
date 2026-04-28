using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Challenge.App.Extensions;
using STGenetics.Challenge.Business.Features.ItemsMenu.Commands.Create;
using STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetAll;
using STGenetics.Challenge.Business.Features.ItemsMenu.Queries.GetById;

namespace STGenetics.Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsMenuController(IMediator mediator) : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromBody] CreateMenuItemCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var query = new GetAllMenuItemsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(Guid id)
        {
            var query = new GetMenuItemByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}