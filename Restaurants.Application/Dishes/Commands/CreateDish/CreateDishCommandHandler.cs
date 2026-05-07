using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> logger,
        IDishRepository dishRepository,
        IRestaurantRepository restaurantRepository,
        IMapper mapper) : IRequestHandler<CreateDishCommand>
    {
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Dish {@Dish}", request);
            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) 
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = mapper.Map<Dish>(request);

            await dishRepository.CreateAsync(dish);
        }
    }
}
