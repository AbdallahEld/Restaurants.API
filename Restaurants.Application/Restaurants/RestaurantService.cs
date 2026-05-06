using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    internal class RestaurantService(IRestaurantRepository restaurantRepository,
        ILogger<RestaurantService> logger) : IRestaurantService
    {
        public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync()
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();

            var restaurantsDTO = restaurants.Select(RestaurantDTO.FormEntity).ToList();

            return restaurantsDTO;
        }

        public async Task<RestaurantDTO?> GetRestaurantByIdAsync(int id)
        {
            logger.LogInformation($"Get Restaurant with id: {id}");
            var restaurant = await restaurantRepository.GetByIdAsync(id);

            var restaurantDTO =  RestaurantDTO.FormEntity(restaurant);

            return restaurantDTO;
        }
    }
}
