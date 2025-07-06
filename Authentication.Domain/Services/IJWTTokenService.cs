using Authentication.Domain.Entities;

namespace Authentication.Domain.Services
{
    public interface IJWTTokenService
    {
        string GenearteToken(UserEntity entity);
    }
}
