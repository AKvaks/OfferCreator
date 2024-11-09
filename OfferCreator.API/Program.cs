using Microsoft.EntityFrameworkCore;
using OfferCreator.Core.Configuration;
using OfferCreator.Core.Entities;
using OfferCreator.Persistance;
using OfferCreator.Persistance.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddCoreServices();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseBlazorFrameworkFiles();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        if (dbContext != null)
        {
            dbContext.Database.SetCommandTimeout(300);
            dbContext.Database.Migrate();

            if (app.Environment.IsDevelopment())
            {
                await SeedItems(dbContext).ConfigureAwait(false);
                await SeedOffers(dbContext).ConfigureAwait(false);
                await SeedOfferItems(dbContext).ConfigureAwait(false);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        throw;
    }
}

app.Run();

static async Task SeedItems(ApplicationDbContext _dbContext)
{
    if (await _dbContext.Items.AnyAsync().ConfigureAwait(false) == false)
    {
        List<Item> items = new List<Item>()
        {
            new() { ItemName = "Laptop", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { ItemName = "Stolno racunalo", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { ItemName = "Monitor", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { ItemName = "Tipkovnica", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { ItemName = "Mis", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };

        await _dbContext.Items.AddRangeAsync(items);
        await _dbContext.SaveChangesAsync();
    }
}

static async Task SeedOffers(ApplicationDbContext _dbContext)
{
    if (await _dbContext.Offers.AnyAsync().ConfigureAwait(false) == false)
    {
        List<Offer> offers = new List<Offer>()
        {
            new() { OfferNumber = 1, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { OfferNumber = 2, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { OfferNumber = 3, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { OfferNumber = 4, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { OfferNumber = 5, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new() { OfferNumber = 6, OfferItems = new List<OfferItem>(), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
        };

        await _dbContext.Offers.AddRangeAsync(offers);
        await _dbContext.SaveChangesAsync();
    }
}

static async Task SeedOfferItems(ApplicationDbContext _dbContext)
{
    if (await _dbContext.OfferItems.AnyAsync().ConfigureAwait(false) == false)
    {
        List<OfferItem> offerItems = new List<OfferItem>()
        {
            new() { OfferId = 1, ItemId = 1, PricePerItem = 1000, Quantity = 2 },
            new() { OfferId = 2, ItemId = 1, PricePerItem = 1699, Quantity = 1 },
            new() { OfferId = 3, ItemId = 2, PricePerItem = 2000, Quantity = 1 },
            new() { OfferId = 3, ItemId = 3, PricePerItem = 250, Quantity = 2 },
            new() { OfferId = 4, ItemId = 2, PricePerItem = 3000, Quantity = 1 },
            new() { OfferId = 4, ItemId = 3, PricePerItem = 400, Quantity = 2 },
            new() { OfferId = 4, ItemId = 4, PricePerItem = 90, Quantity = 1 },
            new() { OfferId = 5, ItemId = 1, PricePerItem = 900, Quantity = 1 },
            new() { OfferId = 5, ItemId = 2, PricePerItem = 1800, Quantity = 1 },
            new() { OfferId = 5, ItemId = 3, PricePerItem = 200, Quantity = 2 },
            new() { OfferId = 5, ItemId = 4, PricePerItem = 90, Quantity = 1 },
            new() { OfferId = 5, ItemId = 5, PricePerItem = 40, Quantity = 1 },
            new() { OfferId = 6, ItemId = 5, PricePerItem = 40, Quantity = 1 }
        };

        await _dbContext.OfferItems.AddRangeAsync(offerItems);
        await _dbContext.SaveChangesAsync();
    }
}