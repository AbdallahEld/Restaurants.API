using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    public static class InfrastructureServices
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantConnection");
            services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging();
            });

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
        }
    }
}
