using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    internal class RestaurantService(IRestaurantRepository restaurantRepository,
        ILogger<RestaurantService> logger) : IRestaurantService
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            logger.LogInformation($"Get Restaurant with id: {id}");
            var restaurant = await restaurantRepository.GetByIdAsync(id);
            return restaurant;
        }
    }
}
