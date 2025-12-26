using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Middlewares;

public class TokenAuthenticationMiddleware(
    RequestDelegate next,
    ILogger<TokenAuthenticationMiddleware> logger,
    IConfiguration config)
{
    private const string ApiToken = "Bearer very_secret_token";

    public async Task Invoke(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrEmpty(authHeader) && authHeader == ApiToken)
        {
            await next(context);
            return;
        }

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader["Bearer ".Length..].Trim();
            if (!string.IsNullOrEmpty(token) && ValidateJwtToken(token, out var principal))
            {
                context.User = principal;
                await next(context);
                return;
            }

            logger.LogWarning("JWT validation failed.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        // no valid auth provided
        logger.LogWarning("Unauthorized access attempted. Missing or invalid Authorization header.");
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private bool ValidateJwtToken(string token, out System.Security.Claims.ClaimsPrincipal principal)
    {
        principal = null!;
        var configuredKey = config["Jwt:Key"] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(configuredKey))
        {
            logger.LogError("JWT key not configured (Jwt:Key).");
            return false;
        }

        byte[] keyBytes;
        using (var sha = SHA256.Create())
        {
            keyBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(configuredKey));
        }

        var signingKey = new SymmetricSecurityKey(keyBytes);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = !string.IsNullOrWhiteSpace(config["Jwt:Issuer"]),
            ValidIssuer = config["Jwt:Issuer"],
            ValidateAudience = !string.IsNullOrWhiteSpace(config["Jwt:Audience"]),
            ValidAudience = config["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        try
        {
            principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            // optional: ensure algorithm matches expected
            if (validatedToken is JwtSecurityToken jwt && jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            logger.LogWarning("JWT algorithm mismatch or token not a JwtSecurityToken.");
            return false;
        }
        catch (Exception ex)
        {
            logger.LogDebug(ex, "Token validation exception");
            return false;
        }
    }
}
