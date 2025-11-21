using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Goodreads.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Register MediatR - ONLY ONCE (remove the duplicate below)
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            // AutoMapper with AddProfile callback
            services.AddAutoMapper(cfg =>
            {
                // Auto-register profiles from assembly
                cfg.AddMaps(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}