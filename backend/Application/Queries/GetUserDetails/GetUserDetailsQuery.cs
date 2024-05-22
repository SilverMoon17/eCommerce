using Contracts.User;
using MediatR;

namespace Application.Queries.GetUserDetails;

public class GetUserDetailsQuery : IRequest<UserDetails>
{
    public string UsernameOrEmail { get; set; }
}