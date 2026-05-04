using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Infrastructure
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantConnection");
            services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        }
    }
}
