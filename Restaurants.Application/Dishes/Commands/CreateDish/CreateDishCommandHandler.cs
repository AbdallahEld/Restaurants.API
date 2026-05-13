using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> logger,
        IDishRepository dishRepository,
        IRestaurantRepository restaurantRepository,
        IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Dish {@Dish}", request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) 
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, RestaurantOperations.Update))
                throw new ForbidException();

            var dish = mapper.Map<Dish>(request);

            return await dishRepository.CreateAsync(dish);
        }
    }
}
