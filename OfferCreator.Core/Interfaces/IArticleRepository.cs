using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Core.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleModel>> GetAllArticlesForDropdown();
        Task<PagedResponse<ArticleModel>> GetAllItemsPaginated(int pageNumber, int pageSize);
        Task<int> AddArticle(ArticleModel item);
        Task<int> UpdateArticle(ArticleModel item);
        Task<int> DeleteArticle(int Id);
        Task<ArticleModel> GetArticleById(int Id);
    }
}
