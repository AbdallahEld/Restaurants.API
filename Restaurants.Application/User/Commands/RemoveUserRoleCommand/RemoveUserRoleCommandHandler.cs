using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class RemoveUserRoleCommandHandler (
        ILogger<RemoveUserRoleCommandHandler> logger,
        UserManager<Restaurants.Domain.User> userManager,
        RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserRoleCommand>
    {
        public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Removing role: {RoleName} from user: {UserEmail}", request.RoleName, request.UserEmail);

            var user = await userManager.FindByEmailAsync(request.UserEmail) 
                ?? throw new NotFoundException(nameof(Restaurants.Domain.User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName) 
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
