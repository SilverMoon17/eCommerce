using Application.Commands.Login;
using Application.Commands.Register;
using Application.Queries.GetUserDetails;
using Contracts.Login;
using Contracts.Register;
using Infrastructure.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly JwtService _jwtService;

    public AccountController(JwtService jwtService, IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(jwtService);
        ArgumentNullException.ThrowIfNull(mediator);
        _jwtService = jwtService;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand()
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            ConfirmationPassword = request.ConfirmationPassword
        };

        var result = await _mediator.Send(command);
        
        return result.IsSuccessful ? Ok(result) : BadRequest(result.ErrorMessage);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand()
        {
            UsernameOrEmail = request.UsernameOrEmail,
            Password = request.Password
        };

        var result = await _mediator.Send(command);

        if (!result.IsSuccessful)
            return BadRequest(result.ErrorMessage);

        var userDetailsQuery = new GetUserDetailsQuery()
        {
            UsernameOrEmail = request.UsernameOrEmail
        };

        var userDetails = await _mediator.Send(userDetailsQuery);

        var jwtToken = _jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);
        
        Response.Cookies.Append(Constants.TokenCookieName, jwtToken, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.MaxValue
        });

        return Ok(jwtToken);
    }
}