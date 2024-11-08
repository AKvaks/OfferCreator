using System.ComponentModel.DataAnnotations;

namespace OfferCreator.Core.Models.DTOs.OfferItems
{
    public class OfferItemModel
    {
        public int Id { get; set; }
        [Required]
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public int OfferId { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per item must be greater than or equal to 0")]
        public decimal PricePerItem { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public decimal Quantity { get; set; }
        public decimal TotalPrice => Math.Round(PricePerItem * Quantity, 2, MidpointRounding.AwayFromZero);
    }
}
