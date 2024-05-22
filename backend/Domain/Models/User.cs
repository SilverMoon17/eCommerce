using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}