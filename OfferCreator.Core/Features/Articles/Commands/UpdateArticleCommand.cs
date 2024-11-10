using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Features.Articles.Commands
{
    public class UpdateArticleCommand : ArticleModel, IRequest<int>;

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;
        public UpdateArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<int> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            return await _articleRepository.UpdateArticle(command);
        }
    }
}
