using Domain.Enums;
using Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, CommandStatus>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(userManager);
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<CommandStatus> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmationPassword)
            return CommandStatus.Failed("The passwords don't match");
        
        var userWithEmailExists = await _dbContext.Users.AnyAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);

        if (userWithEmailExists)
            return CommandStatus.Failed("User with this email already exists");
        
        
        var userWithUsernameExists = await _dbContext.Users.AnyAsync(u => u.UserName == request.Username, cancellationToken: cancellationToken);

        if (userWithUsernameExists)
            return CommandStatus.Failed("User with this username already exists");

        var user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        var roleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

        if (!createUserResult.Succeeded || !roleResult.Succeeded)
            return CommandStatus.Failed("There was an unexpected error on the server side, please contact support");

        return new CommandStatus();
    }
}