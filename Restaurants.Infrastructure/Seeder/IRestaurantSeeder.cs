using Microsoft.AspNetCore.Identity;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    public interface IRestaurantSeeder
    {
        Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager);
    }
}