using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Features.Articles.Queries
{
    public record GetArticleByIdQuery(int Id) : IRequest<ArticleModel>;

    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleModel>
    {
        private readonly IArticleRepository _articleRepository;
        public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<ArticleModel> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _articleRepository.GetArticleById(request.Id);
        }
    }
}
