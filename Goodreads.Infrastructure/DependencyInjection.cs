using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Identity;
using Goodreads.Infrastructure.Persistence;
using Goodreads.Infrastructure.Persistence.Seeders;
using Goodreads.Infrastructure.Repositories;
using Goodreads.Infrastructure.Seeders;
using Goodreads.Infrastructure.Services;
using Goodreads.Infrastructure.Services.EmailService;
using Goodreads.Infrastructure.Services.Storage;
using Goodreads.Infrastructure.Services.TokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace Goodreads.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration)
                .AddIdentity()
                .AddAuthentication(configuration);

            // Use the built-in AddAuthorization method directly
            services.AddAuthorization();

            services.AddEmailServices(configuration);
            services.AddImageServices(configuration); // ✅ ADD THIS LINE

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Seeding
            services.AddScoped<ISeeder, RolesSeeder>();
            services.AddScoped<AppSeeder>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services
                .ConfigureOptions<TokenProviderConfiguration>()
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer();

            services.AddHttpContextAccessor();
            services.AddScoped<ITokenProvider, JwtTokeProvider>();
            services.AddScoped<IUserContext, UserContext>();

            return services;
        }

        private static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.Section));

            var emailSettings = configuration.GetSection(EmailSettings.Section).Get<EmailSettings>();

            if (emailSettings?.UseResend == true)
            {
                // Simple registration without HttpClient factory
                services.AddScoped<IEmailService, ResendApiEmailService>();
                Console.WriteLine("📧 Using Resend API email service");
            }
            else
            {
                services.AddScoped<IEmailService, MailKitEmailService>();
                Console.WriteLine("📧 Using MailKit email service");
            }

            return services;
        }

        // ✅ ADD THIS NEW METHOD
        private static IServiceCollection AddImageServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Cloudinary settings
            services.Configure<CloudinarySettings>(configuration.GetSection(CloudinarySettings.Section));

            // Register the image service (Cloudinary implementation)
            services.AddScoped<IImageService, CloudinaryImageService>();

            Console.WriteLine("🖼️ Using Cloudinary image service");

            return services;
        }
    }
}