using System.ComponentModel.DataAnnotations;

namespace OfferCreator.Core.Models.DTOs.OfferItems
{
    public class OfferItemModel
    {
        public int Id { get; set; }
        public required int ArticleId { get; set; }
        public required string ArticleName { get; set; }
        public int OfferId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per item must be greater than or equal to 0")]
        public decimal PricePerItem { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
        public decimal TotalPrice => PricePerItem * Quantity;
    }
}
