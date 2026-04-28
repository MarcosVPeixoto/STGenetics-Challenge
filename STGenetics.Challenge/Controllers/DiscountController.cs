using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STGenetics.Challenge.Business.Features.Discounts.Commands.Create;
using STGenetics.Challenge.Business.Features.Discounts.Queries.GetAll;
using STGenetics.Challenge.Business.Features.Discounts.Queries.GetById;

namespace STGenetics.Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController(IMediator mediator) : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var query = new GetAllDiscountsQuery();
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById(Guid id)
        {
            var query = new GetDiscountByIdQuery(id);
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}