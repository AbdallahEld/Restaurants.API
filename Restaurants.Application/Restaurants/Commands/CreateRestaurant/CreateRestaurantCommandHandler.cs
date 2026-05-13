using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class CreateRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser =  userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} {UserId} is creating a new restaurant {@Restaurant}",
                currentUser.Email,
                currentUser.Id,
                request);

            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;

            var id = await restaurantRepository.CreateAsync(restaurant);
            return id;
        }
    }
}
