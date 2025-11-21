namespace Goodreads.API.Exttensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentaion(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerWithAuth();
          
            return services;
        }
    }
}
