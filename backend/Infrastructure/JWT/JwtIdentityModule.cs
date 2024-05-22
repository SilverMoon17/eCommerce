using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT;

public static class JwtIdentityModule
{
    public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();

        services.AddSingleton(tokenSettings);
        services.AddTransient<JwtService>();

        var authBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        authBuilder.AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = (context) =>
                {
                    context.Token = context.Request.Cookies[Constants.TokenCookieName];
                    return Task.CompletedTask;
                }
            };
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.Validate(JwtBearerDefaults.AuthenticationScheme);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey))
            };
        });

        return services;
    }
}