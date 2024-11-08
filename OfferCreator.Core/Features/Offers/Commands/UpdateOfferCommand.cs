using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Features.Offers.Commands
{
    public class UpdateOfferCommand : OfferAddEditModel, IRequest<int>
    {
    }

    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, int>
    {
        private readonly IOfferRepository _offerRepository;
        public UpdateOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<int> Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
        {
            return await _offerRepository.UpdatOffer(command);
        }
    }
}
