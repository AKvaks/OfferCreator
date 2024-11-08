using MediatR;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Features.Offers.Commands
{
    public class AddOfferCommand : OfferAddEditModel, IRequest<int>
    {
    }

    public class AddOfferCommandHandler : IRequestHandler<AddOfferCommand, int>
    {
        private readonly IOfferRepository _offerRepository;
        public AddOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<int> Handle(AddOfferCommand command, CancellationToken cancellationToken)
        {
            return await _offerRepository.AddOffer(command);
        }
    }
}
