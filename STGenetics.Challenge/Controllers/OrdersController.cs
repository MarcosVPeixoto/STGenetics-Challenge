using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Challenge.Business.Features.Orders.Commands.Create;
using STGenetics.Challenge.Business.Features.Orders.Commands.Delete;
using STGenetics.Challenge.Business.Features.Orders.Commands.Update;
using STGenetics.Challenge.Business.Features.Orders.Queries.GetAll;
using STGenetics.Challenge.Business.Features.Orders.Queries.GetById;

namespace STGenetics.Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IMediator mediator) : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { OrderId = id };
            await mediator.Send(command);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {            
            await mediator.Send(command);
            return Ok();
        }
    }
}
