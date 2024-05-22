using Contracts.User;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetails>
{
    private readonly UserManager<User> userManager;

    public GetUserDetailsQueryHandler(UserManager<User> userManager)
    {
        ArgumentNullException.ThrowIfNull(userManager);

        this.userManager = userManager;
    }

    public async Task<UserDetails> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.UsernameOrEmail);

        if (user is null)
            user = await userManager.FindByEmailAsync(request.UsernameOrEmail);

        if (user is null)
            return new UserDetails();

        var roleStrings = await userManager.GetRolesAsync(user);

        return new()
        {
            Id = user.Id.ToString(),
            Roles = roleStrings.ToArray(),
            Username = user.UserName,
        };
    }
}