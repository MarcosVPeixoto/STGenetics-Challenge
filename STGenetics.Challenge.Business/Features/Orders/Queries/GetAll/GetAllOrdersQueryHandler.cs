using AutoMapper;
using MediatR;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<GetAllOrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllOrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<GetAllOrderDto>>(orders);
        }
    }
}
