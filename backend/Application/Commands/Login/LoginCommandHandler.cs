using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, CommandStatus>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        ArgumentNullException.ThrowIfNull(signInManager);
        ArgumentNullException.ThrowIfNull(userManager);
        
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<CommandStatus> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
    
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
        }
    
        if (user == null)
            return CommandStatus.Failed("Invalid credentials");

        var passwordStatus = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!passwordStatus.Succeeded)
            return CommandStatus.Failed("Invalid credentials");

        return new CommandStatus();
    }
}