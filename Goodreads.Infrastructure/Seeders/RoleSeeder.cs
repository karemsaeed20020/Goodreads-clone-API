using Goodreads.Domain.Constants;
using Goodreads.Domain.Entities;
using Goodreads.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Goodreads.Infrastructure.Persistence.Seeders;

internal class RolesSeeder : ISeeder
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RolesSeeder> _logger;

    public RolesSeeder(
        ApplicationDbContext dbContext,
        UserManager<User> userManager,
        ILogger<RolesSeeder> logger)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                // 1. Ensure roles exist
                await EnsureRolesExistAsync();

                // 2. Ensure admin user exists
                await EnsureAdminUserExistsAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding roles and admin user");
            throw;
        }
    }

    private async Task EnsureRolesExistAsync()
    {
        var existingRoles = await _dbContext.Roles.Select(r => r.Name).ToListAsync();

        var rolesToAdd = GetRoles()
            .Where(role => !existingRoles.Contains(role.Name))
            .ToList();

        if (rolesToAdd.Any())
        {
            await _dbContext.Roles.AddRangeAsync(rolesToAdd);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Added {Count} roles to database", rolesToAdd.Count);
        }
        else
        {
            _logger.LogInformation("All roles already exist in database");
        }
    }

    private async Task EnsureAdminUserExistsAsync()
    {
        var adminEmail = "admin@goodreads.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true // Important for admin
            };

            var createResult = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
                _logger.LogInformation("✅ Admin user created successfully: {Email}", adminEmail);
            }
            else
            {
                _logger.LogError("❌ Failed to create admin user: {Errors}",
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            // Ensure admin has the Admin role
            var isInRole = await _userManager.IsInRoleAsync(adminUser, Roles.Admin);
            if (!isInRole)
            {
                await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
                _logger.LogInformation("✅ Admin role added to existing user: {Email}", adminEmail);
            }
            else
            {
                _logger.LogInformation("ℹ️ Admin user already exists with correct role: {Email}", adminEmail);
            }
        }
    }

    private List<IdentityRole> GetRoles()
    {
        return new List<IdentityRole>
        {
            new() { Name = Roles.User, NormalizedName = Roles.User.ToUpper() },
            new() { Name = Roles.Admin, NormalizedName = Roles.Admin.ToUpper() }
        };
    }
}