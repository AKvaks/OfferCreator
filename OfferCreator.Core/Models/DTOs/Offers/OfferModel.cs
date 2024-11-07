using OfferCreator.Core.Models.DTOs.OfferItems;

namespace OfferCreator.Core.Models.DTOs.Offers
{
    public class OfferModel
    {
        public int Id { get; set; }
        public int OfferNumber { get; set; }
        public required string DateOfOffer { get; set; }
        public List<OfferItemModel>? OfferItems { get; set; }
    }
}
