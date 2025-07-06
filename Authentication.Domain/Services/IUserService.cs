using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Domain.Services
{
    public interface IUserService
    {
        Task<string> UserRegistrationAsync(RegistrationDto registrationDto);
        Task<string> UserLoginAsync(string userName, string password);
        Task<List<UserEntity>> GetRegistrationDataAsync();
    }
}
