using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application
{
    public static class ApplicationServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(RestaurantService).Assembly;
            services.AddScoped<IRestaurantService, RestaurantService>();

            services.AddAutoMapper(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        }
    }
}
