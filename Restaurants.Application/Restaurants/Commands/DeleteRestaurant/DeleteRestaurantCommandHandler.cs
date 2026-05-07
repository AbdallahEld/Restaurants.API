using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete Restaurant With id : {RestaurantId}", request.Id);
            var restaurant = await restaurantRepository.GetByIdAsync( request.Id );
            if ( restaurant == null )
                return false;

            await restaurantRepository.DeleteAsync(restaurant);
            return true;
        }
    }
}
