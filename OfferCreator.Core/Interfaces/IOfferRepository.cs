using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<OfferListModel>> GetAllOffers();
    }
}
