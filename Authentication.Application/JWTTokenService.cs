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
            var Claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Email, entity.Email),
                    new Claim(ClaimTypes.Name, entity.UserName),
                    // Fix: Convert TokenExpirationInMinutes to a string to match the expected type for Claim value
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(entity.TokenExpirationInMinutes).ToString("o"))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(entity.TokenExpirationInMinutes), // Ensure expiration matches the claim
                signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
