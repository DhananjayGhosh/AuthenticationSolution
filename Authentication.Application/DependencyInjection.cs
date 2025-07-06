using Authentication.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection service)
        {
            service.AddScoped<IWordDocumentRead, WordDocumentRead>();
            service.AddScoped<ICacheService, MemoryCacheService>();
            service.AddScoped<IJWTTokenService, JWTTokenService>();
            service.AddScoped<IUserService, UserService>();
            return service;
        }
    }
}
