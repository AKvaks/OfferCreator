namespace OfferCreator.Core.Entities
{
    public class Offer : BaseEntity
    {
        public int Id { get; set; }
        public required int OfferNumber { get; set; }
        public ICollection<OfferItem>? OfferItems { get; set; }
    }
}
