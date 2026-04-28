using AutoMapper;
using MediatR;
using STGenetics.Challenge.Infra.Interfaces;

namespace STGenetics.Challenge.Business.Features.Discounts.Queries.GetById
{
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, GetDiscountByIdDto>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<GetDiscountByIdDto> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.FindAsync(request.DiscountId);
            return _mapper.Map<GetDiscountByIdDto>(discount);
        }
    }
}
