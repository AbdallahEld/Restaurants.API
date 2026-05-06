

using Restaurants.Domain;

namespace Restaurants.Application
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? AddressCity { get; set; }
        public string? AddressStreet { get; set; }
        public string? AddressPostalCode { get; set; }

        public List<DishDTO> Dishes { get; set; } = new();

        public static RestaurantDTO? FormEntity(Restaurant? restaurant)
        {
            if (restaurant == null) return null;

            return new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                Dishes = restaurant.Dishes.Select(DishDTO.FormEntity).ToList(),
                HasDelivery = restaurant.HasDelivery,
                AddressCity = restaurant.Address?.City,
                AddressStreet = restaurant.Address?.Street,
                AddressPostalCode = restaurant.Address?.PostalCode,
            };
        }
    }
}
