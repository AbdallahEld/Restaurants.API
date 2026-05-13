using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(
        ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize (Restaurant restaurant, RestaurantOperations operation)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail} for operation {Operation} on restaurant {RestaurantName}",
                user.Email, operation, restaurant.Name);

            if (operation == RestaurantOperations.Read || operation == RestaurantOperations.Create)
            {
                logger.LogInformation("Read/Create operation - authorization granted");
                return true;
            }

            if (operation == RestaurantOperations.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user : delete operation - authorization granted");
                return true;
            }

            if((operation == RestaurantOperations.Delete || operation == RestaurantOperations.Update) && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Owner user : {Operation} operation - authorization granted", operation);
                return true;
            }

            logger.LogWarning("User {UserEmail} is not authorized for operation {Operation} on restaurant {RestaurantName}",
                user.Email, operation, restaurant.Name);
            return false;
        }
    }
}
