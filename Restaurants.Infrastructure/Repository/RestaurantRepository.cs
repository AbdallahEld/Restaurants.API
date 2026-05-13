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
        public async Task<IEnumerable<Restaurant>> GetAllMatching(string? searchPhrase)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var restaurants = await dbContext.Restaurants.Include(r => r.Dishes)
                                                         .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                                                               || r.Description.ToLower().Contains(searchPhraseLower)))
                                                         .ToListAsync();

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

        public Task SaveChangesAsync()
            => dbContext.SaveChangesAsync();
    }
}
