using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using STGenetics.Challenge.Domain.Entities;

namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetAll
{
    [AutoMap(typeof(Order))]
    public class GetAllOrderDto
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }        
    }
}