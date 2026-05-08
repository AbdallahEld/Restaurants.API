using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    internal class DishRepository(RestaurantDbContext dbContext) : IDishRepository
    {
        public async Task<int> CreateAsync(Dish dish)
        {
            dbContext.Add(dish);
            await dbContext.SaveChangesAsync();

            return dish.Id;
        }

        public async Task DeleteCollectionAsync(IEnumerable<Dish> dishes)
        {
            dbContext.Dishes.RemoveRange(dishes);
            await dbContext.SaveChangesAsync();
        }
    }
}
