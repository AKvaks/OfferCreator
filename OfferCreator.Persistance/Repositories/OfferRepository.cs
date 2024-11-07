using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.Offers;

namespace OfferCreator.Persistance.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationDbContext _context;
        public OfferRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<OfferListModel>> GetAllOffersPaginated(int pageNumber, int pageSize)
        {
            var offersQuery = _context.Offers
                .Select(x => new OfferListModel
                {
                    Id = x.Id,
                    OfferNumber = x.OfferNumber,
                    DateOfOffer = x.UpdatedAt.ToShortDateString()
                }).AsQueryable();

            var offersCount = await offersQuery.CountAsync();
            var offers = await offersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResponse<OfferListModel>(offers, offersCount, pageNumber, pageSize);
        }

        public async Task<IEnumerable<OfferListModel>> GetAllOffers()
        {
            return await _context.Offers
                .Select(x => new OfferListModel
                {
                    Id = x.Id,
                    OfferNumber = x.OfferNumber,
                    DateOfOffer = x.UpdatedAt.ToShortDateString()
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
