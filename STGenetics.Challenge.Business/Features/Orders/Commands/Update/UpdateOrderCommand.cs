using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Update
{
    public class UpdateOrderCommand : IRequest<UpdateOrderDto>
    {
        public Guid OrderId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}