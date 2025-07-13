using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.DbEntity;

namespace Authentication.Infrastructure.Helper
{
    public static class Mapper
    {
        public static Users MapToUsers(RegistrationDto registration)
        {
            return new Users()
            {
                UserName = registration.UserName,
                Email = registration.Email,
                EmailConfirmed = false,
                PhoneNumber = registration.PhoneNumber,
                PhoneNumberConfirmed = false,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registration.Password),
                IsActive = true,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                TokenExpirationInMinutes = registration.TokenExpirationInMinutes,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static UserEntity MapToUserEntity(Users users)
        {
            return new UserEntity()
            {
                Id = users.Id,
                UserName = users.UserName,
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                TokenExpirationInMinutes = users.TokenExpirationInMinutes
            };
        }
    }
}
