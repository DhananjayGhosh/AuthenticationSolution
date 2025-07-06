using Authentication.Infrastructure;
using Authentication.Application;

namespace Authentication.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureDI(configuration).AddApplicationDI();
            return services;
        }
    }
}
