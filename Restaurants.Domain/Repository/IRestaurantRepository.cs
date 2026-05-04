namespace Restaurants.Domain
{
    public interface IRestaurantRepository
    {
        public Task<IEnumerable<Restaurant>> GetAllAsync();
        public Task<Restaurant?> GetByIdAsync(int id);
    }
}
