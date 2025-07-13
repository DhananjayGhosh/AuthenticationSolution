using Authentication.Domain.Repositories;
using Authentication.Infrastructure.DbEntity;
using Authentication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authentication.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")
                 )
            );
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,                        
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                        )
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var response = new { status = 401, message = "Unauthorized: Please provide a valid token or login again." };
                            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var response = new { status = 403, message = "Forbidden: You do not have permission to access this resource." };
                            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                        }
                    };

                });


            service.AddAuthorization(options =>
            {
                options.AddPolicy("User1OrUser4", policy =>
                    policy.RequireAssertion(context =>
                        context.User.Identity?.Name?.Equals("User1", StringComparison.OrdinalIgnoreCase) == true ||
                        //context.User.Identity?.Name?.Equals("User2", StringComparison.OrdinalIgnoreCase) == true ||
                        context.User.Identity?.Name?.Equals("User4", StringComparison.OrdinalIgnoreCase) == true
                    )
                );


                options.AddPolicy("User2OrUser4", policy =>
                    policy.RequireAssertion(context =>
                    {
                        var name = context.User.Identity?.Name?.ToLower();
                        return name == "user2" || name == "user4";
                    }
                    )
                );

                options.AddPolicy("User1OrUser2OrUser3OrUser4", policy =>
                    policy.RequireAssertion(context =>
                    {
                        var name = context.User.Identity?.Name?.ToLower();
                        return name == "user1" || name == "user2" || name == "user3" || name == "user4";
                    }
                    )
                );
            });
            return service;
        }
    }
}
