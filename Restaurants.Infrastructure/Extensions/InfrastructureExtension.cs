using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Authorization.Services;

namespace Restaurants.Infrastructure
{
    public static class InfrastructureServices
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantConnection");
            services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging();
            });

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim("Nationality", "Egyptian", "German", "Polish"))
                .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.CreatedAtleast2Restaurants, builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantsRequirementHandler>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }
    }
}
