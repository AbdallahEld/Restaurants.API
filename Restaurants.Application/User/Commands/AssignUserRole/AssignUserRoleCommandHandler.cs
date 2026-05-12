using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class AssignUserRoleCommandHandler(
        ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<Restaurants.Domain.User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning role {UserRole} to user with email {UserEmail}", request.UserRole, request.UserEmail);
            
            var user = await userManager.FindByEmailAsync(request.UserEmail) 
                ?? throw new NotFoundException(nameof(Restaurants.Domain.User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.UserRole)
                ?? throw new NotFoundException(nameof(IdentityRole), request.UserRole);

            await userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
