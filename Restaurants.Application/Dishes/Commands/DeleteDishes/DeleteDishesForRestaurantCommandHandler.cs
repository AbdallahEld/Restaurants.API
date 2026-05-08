using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class DeleteDishesForRestaurantCommandHandler(
        ILogger<DeleteDishesForRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository,
        IDishRepository dishRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dishes for restaurant with id {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await dishRepository.DeleteCollectionAsync(restaurant.Dishes);
        }
    }
}
