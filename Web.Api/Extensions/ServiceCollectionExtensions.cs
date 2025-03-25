using HogwartsAPI.Infrastructure.Database;
using HogwartsAPI.Infrastructure.Repositories;
using HogwartsAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Web.Api.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string? connectionStr)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr));
            });
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<HogwartsApiRepository>();
            services.AddScoped<HogwartsApiService>();
            services.AddScoped<HousesRepository>();
            return services;
        }

        public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}