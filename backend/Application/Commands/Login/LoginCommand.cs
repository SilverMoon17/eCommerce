using MediatR;

namespace Application.Commands.Login;

public class LoginCommand : IRequest<CommandStatus>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}