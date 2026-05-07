using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class GetRestaurantByIdQueryHandler (
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IRestaurantRepository restaurantRepository,
        IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO?>
    {
        public async Task<RestaurantDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get Restaurant with id: {RestaurantId}", request.Id);
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);

            return restaurantDTO;
        }
    }
}
