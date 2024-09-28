using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Utilities.Security
{
    public static class JwtTokenHelper
    {
        public static string GenerateToken(string secretKey, Claim[] claims, int expirationMinutes, string? issuer = null, string? audience = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public static ClaimsPrincipal ValidateToken(string token, string secretKey, bool validateIssuer = false, string? issuer = null, bool validateAudience = false, string? audience = null, bool validateLifetime = true)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = validateIssuer,
                ValidIssuer = issuer,
                ValidateAudience = validateAudience,
                ValidAudience = audience,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero 
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        public static bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            return jwtToken.ValidTo < DateTime.UtcNow;
        }

        public static DateTime? GetExpirationDate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken?.ValidTo;
        }

        public static IEnumerable<Claim>? GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            return jwtToken?.Claims;
        }
    }
}
