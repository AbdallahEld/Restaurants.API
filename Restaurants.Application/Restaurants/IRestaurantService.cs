using Restaurants.Domain;

namespace Restaurants.Application
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync();
        Task<RestaurantDTO?> GetRestaurantByIdAsync(int id);
    }
}