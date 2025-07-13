using Authentication.Domain.DTOs;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Domain.Services;
using Authentication.Infrastructure.Helper;

namespace Authentication.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly ICacheService _cacheService;

        public UserService(IUserRepository repository, IJWTTokenService jWTTokenService, ICacheService cacheService)
        {
            _repository = repository;
            _jwtTokenService = jWTTokenService;
            _cacheService = cacheService;
        }

        public async Task<List<UserEntity>> GetRegistrationDataAsync()
        {
            var cachekey = CacheKeys.RegistrationData;
            var cached = await _cacheService.GetAsync<List<UserEntity>>(cachekey);
            if (cached != null)
                return cached;
            var resList = await _repository.GetRegistrationDataAsync();
            await _cacheService.SetAsync(cachekey, resList, TimeSpan.FromMinutes(10));
            return resList;

        }

        public async Task<string> UserLoginAsync(string userName, string password)
        {
            var userEntity = await _repository.UserLoginAsync(userName, password);
            if (userEntity == null) 
            {
                return string.Empty;
            }
            return _jwtTokenService.GenearteToken(userEntity);
           
        }

        public async Task<string> UserRegistrationAsync(RegistrationDto registrationDto)
        {
            return await _repository.UserRegistrationAsync(registrationDto);
        }

    }
}
