namespace Restaurants.Domain
{
    public interface IDishRepository
    {
        public Task<int> CreateAsync(Dish dish);
    }
}
