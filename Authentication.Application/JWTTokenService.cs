using Authentication.Domain.Entities;
using Authentication.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Application
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly IConfiguration _configuration;

        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenearteToken(UserEntity entity)
        {
            var Claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
              new Claim(ClaimTypes.Email, entity.Email),
              new Claim(ClaimTypes.Name, entity.UserName)
            };
            // Add expiration claim only if TokenExpirationInMinutes is set
            if (entity.TokenExpirationInMinutes.HasValue)
            {
                Claims.Add(new Claim(
                    ClaimTypes.Expiration,
                    DateTime.UtcNow.AddMinutes(entity.TokenExpirationInMinutes.Value).ToString("o")
                ));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: entity.TokenExpirationInMinutes.HasValue
               ? DateTime.UtcNow.AddMinutes(entity.TokenExpirationInMinutes.Value)
               : DateTime.UtcNow.AddMinutes(60), // unlimited access (no expiry)
                signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
