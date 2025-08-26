using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register API services here
            // Example: services.AddControllers();
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            // Configure API middleware here
            // Example: app.UseRouting();
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            return app;
        }
    }
}
