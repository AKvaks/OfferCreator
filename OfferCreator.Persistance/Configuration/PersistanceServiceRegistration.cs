using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfferCreator.Core.Interfaces;
using OfferCreator.Persistance.Repositories;

namespace OfferCreator.Persistance.Configuration
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            return services;
        }
    }
}
