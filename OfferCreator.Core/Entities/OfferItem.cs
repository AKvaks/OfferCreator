using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfferCreator.Core.Entities
{
    public class OfferItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per item must be greater than or equal to 0")]
        public decimal PricePerItem { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }

        public decimal TotalPrice => PricePerItem * Quantity;

        [Required]
        [ForeignKey(nameof(Offer))]
        public required int OfferId { get; set; }

        [Required]
        public Offer Offer { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public required int ItemId { get; set; }

        [Required]
        public Item Item { get; set; }
    }
}
