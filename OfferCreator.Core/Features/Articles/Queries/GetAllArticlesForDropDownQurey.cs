using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Features.Articles.Queries
{
    public record GetAllArticlesForDropDownQurey : IRequest<IEnumerable<ArticleModel>>;

    public class GetAllArticlesForDropDownQureyHandler : IRequestHandler<GetAllArticlesForDropDownQurey, IEnumerable<ArticleModel>>
    {
        private readonly IArticleRepository _articleRepository;
        public GetAllArticlesForDropDownQureyHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<IEnumerable<ArticleModel>> Handle(GetAllArticlesForDropDownQurey request, CancellationToken cancellationToken)
        {
            return await _articleRepository.GetAllArticlesForDropdown();
        }
    }
}
