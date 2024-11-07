using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Interfaces;
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

        public async Task<IEnumerable<OfferListModel>> GetAllOffers()
        {
            return await _context.Offers
                .Select(x => new OfferListModel
                {
                    Id = x.Id,
                    OfferNumber = x.OfferNumber,
                    DateOfOffer = x.UpdatedAt.ToShortDateString()
                }).ToListAsync();
        }
    }
}
