using System.ComponentModel.DataAnnotations;

namespace OfferCreator.Core.Models.DTOs.Articles
{
    public class ArticleModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Article name is required")]
        public string ArticleName { get; set; }
    }
}
