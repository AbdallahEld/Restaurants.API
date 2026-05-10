using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class UpdateUserDetailsCommandHandler(
        ILogger<UpdateUserDetailsCommandHandler> logger,
        IUserContext userContext,
        IUserStore<Restaurants.Domain.User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Updating user: {UserId} with {@Request}", user!.Id, request);

            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if ( dbUser is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.User), user!.Id);
            }

            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
