using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LodgerBackend.Auth;

public class JwtMiddleware(IConfiguration configuration, RequestDelegate next)
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly byte[] _key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException());
    private SecurityToken? _validatedToken;

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api") &&
            !context.Request.Path.StartsWithSegments("/swagger") &&
            !context.Request.Path.StartsWithSegments("/api/auth/login") &&
            !context.Request.Path.StartsWithSegments("/api/auth/register") &&
            !context.Request.Path.StartsWithSegments("/api/files"))
        {
            var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_key)
                    };
                    
                    _tokenHandler.ValidateToken(token, tokenValidationParameters, out _validatedToken);

                    if (_validatedToken != null && _validatedToken.ValidTo < DateTime.UtcNow)
                    {
                        // Token expiré
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }

                    context.Items["JwtToken"] = _validatedToken;
                }
                catch (Exception)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await next(context);
    }
}