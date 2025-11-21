using Goodreads.API.Exttensions;
using Goodreads.Application;
using Goodreads.Infrastructure;
using Goodreads.Infrastructure.Seeders;
using Scalar.AspNetCore;
using System.Threading.Tasks;

namespace Goodreads.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Data Protection
            builder.Services.AddDataProtection();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Register your layers - MAKE SURE THESE ARE CALLED
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration); // This registers IEmailService
            builder.Services.AddSwaggerWithAuth();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "Goodreads API";
                    options.OpenApiRoutePattern = "/swagger/v1/swagger.json";
                });
            }
            // Seed database
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<AppSeeder>();
                await seeder.SeedAsync();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}