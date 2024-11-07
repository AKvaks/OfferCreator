using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Core.Interfaces
{
    public interface IOfferRepository
    {
        Task<IEnumerable<OfferListModel>> GetAllOffers();
        Task<PagedResponse<OfferListModel>> GetAllOffersPaginated(int pageNumber, int pageSize);
        Task<OfferModel> GetOfferDetailsById(int id);
        Task<OfferAddEditModel> GetOfferForEditById(int id);
        Task<int> AddOffer(OfferAddEditModel offerToAdd);
        Task<int> UpdatOffer(OfferAddEditModel offerToUpdate);
        Task<int> DeleteOffer(int offerId);
    }
}
