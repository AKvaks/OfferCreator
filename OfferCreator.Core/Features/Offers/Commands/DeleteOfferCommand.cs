using MediatR;
using OfferCreator.Core.Interfaces;

namespace OfferCreator.Core.Features.Offers.Commands
{
    public record DeleteOfferCommand(int Id) : IRequest<int>;

    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, int>
    {
        private readonly IOfferRepository _offerRepository;
        public DeleteOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<int> Handle(DeleteOfferCommand command, CancellationToken cancellationToken)
        {
            return await _offerRepository.DeleteOffer(command.Id);
        }
    }
}
