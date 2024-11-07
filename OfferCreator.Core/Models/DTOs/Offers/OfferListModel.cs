namespace OfferCreator.Core.Models.DTOs.Offers
{
    public class OfferListModel
    {
        public int Id { get; set; }
        public int OfferNumber { get; set; }
        public required string DateOfOffer { get; set; }
    }
}
