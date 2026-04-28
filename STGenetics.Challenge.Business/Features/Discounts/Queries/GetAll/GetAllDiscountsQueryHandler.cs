using AutoMapper;
using MediatR;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Discounts.Queries.GetAll
{
    public class GetAllDiscountsQueryHandler : IRequestHandler<GetAllDiscountsQuery, List<GetAllDiscountDto>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetAllDiscountsQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllDiscountDto>> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
        {
            var discounts = await _discountRepository.GetAllAsync();
            return _mapper.Map<List<GetAllDiscountDto>>(discounts);
        }
    }
}
