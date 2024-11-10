using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Entities;
using OfferCreator.Core.Interfaces;
using OfferCreator.Core.Models;
using OfferCreator.Core.Models.DTOs.OfferItems;
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

        public async Task<OfferModel> GetOfferDetailsById(int id)
        {
            var offer = await _context.Offers
                .Include(x => x.OfferItems)
                .ThenInclude(x => x.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (offer != null)
            {
                List<OfferItemModel> offerItemsResult = new List<OfferItemModel>();
                if (offer.OfferItems != null && offer.OfferItems.Any())
                {
                    foreach (var offerItem in offer.OfferItems)
                    {
                        OfferItemModel item = new OfferItemModel
                        {
                            Id = offerItem.Id,
                            ArticleId = offerItem.ItemId,
                            ArticleName = offerItem.Item.ItemName,
                            OfferId = offer.Id,
                            PricePerItem = offerItem.PricePerItem,
                            Quantity = offerItem.Quantity
                        };
                        offerItemsResult.Add(item);
                    }
                }

                return new OfferModel
                {
                    Id = offer.Id,
                    OfferNumber = offer.OfferNumber,
                    DateOfOffer = offer.UpdatedAt.ToShortDateString(),
                    OfferItems = offerItemsResult
                };
            }

            return new OfferModel
            {
                Id = -1,
                OfferNumber = -1,
                DateOfOffer = DateTime.UtcNow.ToShortDateString(),
                OfferItems = null
            };
        }

        public async Task<OfferAddEditModel> GetOfferForEditById(int id)
        {
            var offer = await _context.Offers
                .Include(x => x.OfferItems)
                .ThenInclude(x => x.Item)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (offer != null)
            {
                List<OfferItemModel> offerItemsResult = new List<OfferItemModel>();
                if (offer.OfferItems != null && offer.OfferItems.Any())
                {
                    foreach (var offerItem in offer.OfferItems)
                    {
                        OfferItemModel item = new OfferItemModel
                        {
                            Id = offerItem.Id,
                            ArticleId = offerItem.ItemId,
                            ArticleName = offerItem.Item.ItemName,
                            OfferId = offer.Id,
                            PricePerItem = offerItem.PricePerItem,
                            Quantity = offerItem.Quantity
                        };
                        offerItemsResult.Add(item);
                    }
                }

                return new OfferAddEditModel
                {
                    Id = offer.Id,
                    OfferNumber = offer.OfferNumber,
                    DateOfOffer = offer.UpdatedAt.ToShortDateString(),
                    OfferItems = offerItemsResult,
                    OfferItemsIdsToDelete = new List<int>()
                };
            }

            return new OfferAddEditModel
            {
                Id = -1,
                OfferNumber = -1,
                DateOfOffer = DateTime.UtcNow.ToShortDateString(),
                OfferItems = null,
                OfferItemsIdsToDelete = null
            };
        }

        public async Task<int> AddOffer(OfferAddEditModel offerToAdd)
        {
            var offerNumber = await _context.Offers.CountAsync();
            Offer offer = new Offer()
            {
                OfferNumber = offerNumber + 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();

            if (offerToAdd.OfferItems != null)
            {
                List<OfferItem> offerItemsToAdd = new List<OfferItem>();
                foreach (var offerItem in offerToAdd.OfferItems)
                {
                    OfferItem item = new OfferItem()
                    {
                        OfferId = offer.Id,
                        ItemId = offerItem.ArticleId,
                        PricePerItem = offerItem.PricePerItem,
                        Quantity = offerItem.Quantity
                    };
                    offerItemsToAdd.Add(item);
                }

                await _context.OfferItems.AddRangeAsync(offerItemsToAdd);
                await _context.SaveChangesAsync();
            }

            return offer.Id;
        }

        public async Task<int> UpdatOffer(OfferAddEditModel offerToUpdate)
        {
            if (offerToUpdate.OfferItemsIdsToDelete != null && offerToUpdate.OfferItemsIdsToDelete.Any())
            {
                var offerItemsToDelete = await _context.OfferItems
                .Where(x => offerToUpdate.OfferItemsIdsToDelete.Contains(x.Id))
                .ToListAsync();

                _context.RemoveRange(offerItemsToDelete);
                await _context.SaveChangesAsync();
            }

            var offer = await _context.Offers
                .Include(x => x.OfferItems)
                .FirstOrDefaultAsync(x => x.Id == offerToUpdate.Id);

            if (offer != null)
            {
                if (offerToUpdate.OfferItems != null && offerToUpdate.OfferItems.Any())
                {
                    List<OfferItem> offerItemsToAdd = new List<OfferItem>();
                    foreach (var offerItem in offerToUpdate.OfferItems)
                    {
                        var offerItemToUpdate = offer.OfferItems?.FirstOrDefault(x => x.Id == offerItem.Id);
                        if (offerItemToUpdate != null)
                        {
                            offerItemToUpdate.ItemId = offerItem.ArticleId;
                            offerItemToUpdate.PricePerItem = offerItem.PricePerItem;
                            offerItemToUpdate.Quantity = offerItem.Quantity;
                        }
                        else
                        {
                            OfferItem offerToAdd = new OfferItem()
                            {
                                ItemId = offerItem.ArticleId,
                                OfferId = offer.Id,
                                PricePerItem = offerItem.PricePerItem,
                                Quantity = offerItem.Quantity
                            };

                            offerItemsToAdd.Add(offerToAdd);
                        }
                    }

                    if (offerItemsToAdd.Count > 0) await _context.OfferItems.AddRangeAsync(offerItemsToAdd);
                }

                offer.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return offer.Id;
            }

            return -1;
        }

        public async Task<int> DeleteOffer(int offerId)
        {
            var itemToDelete = await _context.Offers
                .Include(x => x.OfferItems)
                .FirstOrDefaultAsync(x => x.Id == offerId);

            if (itemToDelete != null)
            {
                _context.Offers.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                return itemToDelete.Id;
            }

            return -1;
        }
    }
}
