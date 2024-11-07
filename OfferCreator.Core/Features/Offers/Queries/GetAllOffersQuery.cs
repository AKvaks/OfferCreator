using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Features.Offers.Queries
{
    public record GetAllOffersQuery : IRequest<IEnumerable<OfferListModel>>;

    public class GetAllOffersQueryHandler : IRequestHandler<GetAllOffersQuery, IEnumerable<OfferListModel>>
    {
        private readonly IOfferRepository _offerRepository;
        public GetAllOffersQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<OfferListModel>> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
        {
            return await _offerRepository.GetAllOffers();
        }
    }
}
