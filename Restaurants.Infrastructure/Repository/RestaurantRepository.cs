using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.Include(r => r.Dishes).ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                                            .Include(r => r.Dishes)
                                            .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }
    }
}
