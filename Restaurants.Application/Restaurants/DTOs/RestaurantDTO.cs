

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
    }
}
