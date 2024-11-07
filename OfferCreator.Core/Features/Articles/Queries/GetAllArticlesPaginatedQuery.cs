using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Features.Articles.Queries
{
    public record GetAllArticlesPaginatedQuery(int? PageNumber, int? PageSize) : IRequest<PagedResponse<ArticleModel>>;

    public class GetAllArticlesPaginatedQueryHandler : IRequestHandler<GetAllArticlesPaginatedQuery, PagedResponse<ArticleModel>>
    {
        private readonly IArticleRepository _itemRepository;
        public GetAllArticlesPaginatedQueryHandler(IArticleRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<PagedResponse<ArticleModel>> Handle(GetAllArticlesPaginatedQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = 1;
            if (request.PageNumber.HasValue && request.PageNumber.Value > 0) pageNumber = request.PageNumber.Value;

            var pageSize = 3;
            if (request.PageSize.HasValue && request.PageSize.Value > 0) pageSize = request.PageSize.Value;

            return await _itemRepository.GetAllItemsPaginated(pageNumber, pageSize);
        }
    }
}
