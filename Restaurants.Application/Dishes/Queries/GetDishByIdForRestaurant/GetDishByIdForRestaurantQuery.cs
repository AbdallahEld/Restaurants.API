using MediatR;

namespace Restaurants.Application
{
    public class GetDishByIdForRestaurantQuery(int dishId, int restaurantId) : IRequest<DishDTO>
    {
        public int DishId { get; } = dishId;
        public int RestaurantId { get; } = restaurantId;
    }
}
