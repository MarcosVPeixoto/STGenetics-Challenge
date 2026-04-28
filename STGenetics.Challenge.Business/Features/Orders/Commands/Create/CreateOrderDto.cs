using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Orders.Commands.Create
{
    [AutoMap(typeof(Order))]
    public class CreateOrderDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}