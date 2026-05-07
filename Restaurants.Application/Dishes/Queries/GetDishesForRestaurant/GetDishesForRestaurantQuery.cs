using MediatR;

namespace Restaurants.Application
{
    public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDTO>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
