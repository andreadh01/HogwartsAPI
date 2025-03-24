

namespace HogwartsAPI.Web.Api.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string? connectionStr)
        {
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddGlobalErrorHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}