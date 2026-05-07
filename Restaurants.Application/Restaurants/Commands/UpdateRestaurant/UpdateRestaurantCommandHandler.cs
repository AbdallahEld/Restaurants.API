using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class UpdateRestaurantCommandHandler (
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository,
        IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Update Restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await restaurantRepository.GetByIdAsync( request.Id );
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            mapper.Map(request, restaurant);

            await restaurantRepository.SaveChangesAsync();
        }
    }
}
