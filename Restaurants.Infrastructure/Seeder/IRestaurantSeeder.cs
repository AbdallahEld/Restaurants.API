using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    public interface IRestaurantSeeder
    {
        Task Seed();
    }
}