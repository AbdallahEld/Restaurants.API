using MediatR;

namespace Restaurants.Application
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDTO>
    {
        public int Id { get; } = id;
    }
}
