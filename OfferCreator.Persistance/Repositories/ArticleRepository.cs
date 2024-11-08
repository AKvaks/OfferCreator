using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Entities;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Articles;

namespace OfferCreator.Persistance.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddArticle(ArticleModel item)
        {
            var itemToAdd = new Item()
            {
                ItemName = item.ArticleName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _context.Items.AddAsync(itemToAdd);
            await _context.SaveChangesAsync();

            return itemToAdd.Id;
        }

        public async Task<int> DeleteArticle(int Id)
        {
            var itemToDelete = await _context.Items.FindAsync(Id);
            if (itemToDelete != null)
            {
                itemToDelete.IsDeleted = true;
                itemToDelete.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return itemToDelete.Id;
            }

            return -1;
        }

        public async Task<ArticleModel> GetArticleById(int Id)
        {
            ArticleModel result = new ArticleModel();

            var itemToGet = await _context.Items.FindAsync(Id);
            if (itemToGet != null)
            {
                result.Id = itemToGet.Id;
                result.ArticleName = itemToGet.ItemName;
            }
            else
            {
                result.Id = -1;
            }

            return result;
        }

        public async Task<IEnumerable<ArticleModel>> GetAllArticlesForDropdown()
        {
            return await _context.Items.Select(x => new ArticleModel
            {
                Id = x.Id,
                ArticleName = x.ItemName
            })
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<PagedResponse<ArticleModel>> GetAllItemsPaginated(int pageNumber, int pageSize)
        {
            var itemsQuery = _context.Items
                .Where(x => !x.IsDeleted)
                .Select(x => new ArticleModel
                {
                    Id = x.Id,
                    ArticleName = x.ItemName
                }).AsQueryable();

            var itemsCount = await itemsQuery.CountAsync();
            var items = await itemsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResponse<ArticleModel>(items, itemsCount, pageNumber, pageSize);
        }

        public async Task<int> UpdateArticle(ArticleModel item)
        {
            var itemToUpdate = await _context.Items.FindAsync(item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate.ItemName = item.ArticleName;
                itemToUpdate.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return itemToUpdate.Id;
            }

            return -1;
        }
    }
}
