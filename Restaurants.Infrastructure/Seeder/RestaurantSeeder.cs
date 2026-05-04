using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure
{
    internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!await dbContext.Restaurants.AnyAsync())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new()
{
    new Restaurant
    {
        Name = "Pizza Palace",
        Description = "Authentic Italian pizza with fresh ingredients",
        Category = "Italian",
        HasDelivery = true,
        ContactEmail = "info@pizzapalace.com",
        ContactNumber = "01012345678",
        Address = new Address
        {
            City = "Cairo",
            Street = "Tahrir Street 12",
            PostalCode = "11511"
        },
        Dishes = new List<Dish>
        {
            new Dish { Name = "Margherita", Description = "Classic pizza with tomato and mozzarella", Price = 80, RestaurantId = 1 },
            new Dish { Name = "Pepperoni", Description = "Spicy pepperoni with cheese", Price = 95, RestaurantId = 1 }
        }
    },
    new Restaurant
    {
        Name = "Sushi World",
        Description = "Fresh sushi and Japanese cuisine",
        Category = "Japanese",
        HasDelivery = false,
        ContactEmail = "contact@sushiworld.com",
        ContactNumber = "01098765432",
        Address = new Address
        {
            City = "Alexandria",
            Street = "Corniche Road 45",
            PostalCode = "21500"
        },
        Dishes = new List<Dish>
        {
            new Dish { Name = "California Roll", Description = "Crab, avocado, cucumber", Price = 120, RestaurantId = 2 },
            new Dish { Name = "Salmon Nigiri", Description = "Fresh salmon over rice", Price = 150, RestaurantId = 2 }
        }
    },
    new Restaurant
    {
        Name = "Grill House",
        Description = "Steaks and BBQ specialties",
        Category = "American",
        HasDelivery = true,
        ContactEmail = "order@grillhouse.com",
        ContactNumber = "01122334455",
        Address = new Address
        {
            City = "Giza",
            Street = "Pyramids Street 99",
            PostalCode = "12556"
        },
        Dishes = new List<Dish>
        {
            new Dish { Name = "Ribeye Steak", Description = "Juicy ribeye grilled to perfection", Price = 250, RestaurantId = 3 },
            new Dish { Name = "BBQ Ribs", Description = "Slow cooked ribs with BBQ sauce", Price = 220, RestaurantId = 3 }
        }
    }
};
            return restaurants;

        }
    }
}
