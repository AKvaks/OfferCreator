namespace OfferCreator.Core.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        //public int CreatedById { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public int UpdatedById { get; set; }
    }
}
