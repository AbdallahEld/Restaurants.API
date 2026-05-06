using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application
{
    public static class ApplicationServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();

            services.AddAutoMapper(typeof(RestaurantService).Assembly);
        }
    }
}
