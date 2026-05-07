using FluentValidation;

namespace Restaurants.Application
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(r => r.Name)
                .Length(3, 100);
        }
    }
}
