using MediatR;

namespace Restaurants.Application
{
    public class DeleteRestaurantCommand(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }
}
