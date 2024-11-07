using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Features.Offers.Queries
{
    public record GetAllOffersPaginatedQuery(int? PageNumber, int? PageSize) : IRequest<PagedResponse<OfferListModel>>;

    public class GetAllOffersPaginatedQueryHandler : IRequestHandler<GetAllOffersPaginatedQuery, PagedResponse<OfferListModel>>
    {
        private readonly IOfferRepository _offerRepository;
        public GetAllOffersPaginatedQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<PagedResponse<OfferListModel>> Handle(GetAllOffersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = 1;
            if (request.PageNumber.HasValue && request.PageNumber.Value > 0) pageNumber = request.PageNumber.Value;

            var pageSize = 3;
            if (request.PageSize.HasValue && request.PageSize.Value > 0) pageSize = request.PageSize.Value;

            return await _offerRepository.GetAllOffersPaginated(pageNumber, pageSize);
        }
    }
}
