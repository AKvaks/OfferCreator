namespace OfferCreator.Core.Entities
{
    public class Item : BaseEntity
    {
        public int Id { get; set; }
        public required string ItemName { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<OfferItem>? OfferItems { get; set; }
    }
}
