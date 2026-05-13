using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application
{
    public class DeleteDishesForRestaurantCommandHandler(
        ILogger<DeleteDishesForRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository,
        IDishRepository dishRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dishes for restaurant with id {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);

            if (restaurant == null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, RestaurantOperations.Update))
                throw new ForbidException();

            await dishRepository.DeleteCollectionAsync(restaurant.Dishes);
        }
    }
}
