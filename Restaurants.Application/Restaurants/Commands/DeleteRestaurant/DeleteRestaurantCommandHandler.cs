using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Delete Restaurant With id : {RestaurantId}", request.Id);
            var restaurant = await restaurantRepository.GetByIdAsync( request.Id );
            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            await restaurantRepository.DeleteAsync(restaurant);
        }
    }
}
