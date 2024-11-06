using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Entities;

namespace OfferCreator.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }

        //I know Entity Framework infers a lot of things, but I wanted 
        //to specify some things to be sure
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.ItemName).IsRequired();
                entity.Property(h => h.IsDeleted).HasDefaultValue(false).IsRequired();
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.OfferNumber).IsRequired();
            });

            modelBuilder.Entity<OfferItem>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.PricePerItem).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(h => h.Quantity).HasColumnType("decimal(18,2)").IsRequired();
            });

            modelBuilder.Entity<OfferItem>()
                .HasOne(h => h.Offer)
                .WithMany(h => h.OfferItems)
                .HasForeignKey(h => h.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OfferItem>()
                .HasOne(h => h.Item)
                .WithMany(h => h.OfferItems)
                .HasForeignKey(h => h.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
