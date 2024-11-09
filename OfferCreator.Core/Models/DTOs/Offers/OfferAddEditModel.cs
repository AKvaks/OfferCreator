using OfferCreator.Core.Models.DTOs.OfferItems;

namespace OfferCreator.Core.Models.DTOs.Offers
{
    public class OfferAddEditModel
    {
        public int Id { get; set; }
        public int OfferNumber { get; set; }
        public string? DateOfOffer { get; set; }
        public List<OfferItemModel>? OfferItems { get; set; }
        public List<int>? OfferItemsIdsToDelete { get; set; }
    }
}
