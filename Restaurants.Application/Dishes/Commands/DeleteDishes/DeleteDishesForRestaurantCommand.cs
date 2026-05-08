using MediatR;

namespace Restaurants.Application
{
    public class DeleteDishesForRestaurantCommand (int restaurantId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
