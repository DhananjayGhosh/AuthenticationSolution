using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<string> UserRegistrationAsync(RegistrationDto registrationDto);
        Task<UserEntity?> UserLoginAsync(string userName, string password);
        Task<List<UserEntity>> GetRegistrationDataAsync();
    }
}
