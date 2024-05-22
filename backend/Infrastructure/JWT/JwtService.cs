using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT;

public class JwtService
{
    public readonly TokenSettings tokenSettings;

    public JwtService(TokenSettings tokenSettings)
    {
        ArgumentNullException.ThrowIfNull(tokenSettings);
        
        this.tokenSettings = tokenSettings;
    }

    public string CreateAuthToken(string userId, string username, string[] roles)
    {
        List<Claim> claims = new()
        {
            new Claim(Constants.UsernameClaimName, username),
            new Claim(Constants.UserIdClaimName, userId)
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
        var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature);
        
        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(tokenSettings.ExpirationInMinutes)),
            signingCredentials: signinCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}