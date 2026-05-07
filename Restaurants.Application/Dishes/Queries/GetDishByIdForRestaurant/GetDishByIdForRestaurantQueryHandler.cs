using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class GetDishByIdForRestaurantQueryHandler(
        ILogger<GetDishByIdForRestaurantQueryHandler> logger,
        IRestaurantRepository restaurantRepository,
        IMapper mapper): IRequestHandler<GetDishByIdForRestaurantQuery, DishDTO>
    {
        public async Task<DishDTO> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {dishId}, for restaurant with id: {restaurantId}", request.DishId, request.RestaurantId);

            var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) 
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null)
                throw new NotFoundException(nameof(Restaurant), request.DishId.ToString());

            var result = mapper.Map<DishDTO>(dish);
            return result;
        }
    }
}
