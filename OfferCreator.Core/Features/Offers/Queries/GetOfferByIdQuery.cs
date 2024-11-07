using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Features.Offers.Queries
{
    public record GetOfferByIdQuery(int Id) : IRequest<OfferModel>;

    public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, OfferModel>
    {
        private readonly IOfferRepository _offerRepository;
        public GetOfferByIdQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<OfferModel> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
        {
            return await _offerRepository.GetOfferDetailsById(request.Id);
        }
    }
}
