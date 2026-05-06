using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application
{
    public class CreateRestaurantDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Category Name Is Required")]
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? ContactEmail { get; set; }
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? ContactNumber { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code (XX-XXX).")]
        public string? PostalCode { get; set; }
    }
}
