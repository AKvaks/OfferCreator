using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Features.Articles.Commands
{
    public record AddArticleCommand(string ItemName) : IRequest<int>;

    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;
        public AddArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<int> Handle(AddArticleCommand command, CancellationToken cancellationToken)
        {
            return await _articleRepository.AddArticle(new ArticleModel() { ArticleName = command.ItemName });
        }
    }
}
