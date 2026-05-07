namespace Restaurants.Domain
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>> GetAllAsync();
        public Task<Restaurant?> GetByIdAsync(int id);
        public Task<int> CreateAsync(Restaurant restaurant);
        public Task DeleteAsync(Restaurant restaurant);
        public Task SaveChangesAsync();
    }
}
