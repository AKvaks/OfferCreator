using MediatR;
using OfferCreator.Core.Interfaces;

namespace OfferCreator.Core.Features.Articles.Commands
{
    public record DeleteArticleCommand(int Id) : IRequest<int>;

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, int>
    {
        private readonly IArticleRepository _articleRepository;
        public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<int> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
        {
            return await _articleRepository.DeleteArticle(command.Id);
        }
    }
}
