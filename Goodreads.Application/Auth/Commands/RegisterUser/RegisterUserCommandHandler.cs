using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Goodreads.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Goodreads.Application.Auth.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(
            UserManager<User> userManager,
            IMapper mapper,
            ILogger<RegisterUserCommandHandler> logger,
            IUnitOfWork unitOfWork,
            IEmailService emailService) // ← Fixed parameter name
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService; // ← Now this assignment works
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling RegisterUserCommand for user: {UserName}", request.UserName);

            if (await _userManager.FindByNameAsync(request.UserName) != null)
            {
                return Result<string>.Fail(UserErrors.UsernameTaken);
            }

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return Result<string>.Fail(UserErrors.EmailAlreadyRegistered);

            var user = _mapper.Map<User>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return Result<string>.Fail(UserErrors.CreateUserFailed(result.Errors.First().Description));

            await _userManager.AddToRoleAsync(user, Roles.User);
            await _unitOfWork.SaveChangesAsync();

            // Try to send email, but don't fail registration if it fails
            // Try to send email, but don't fail registration if it fails
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Use UrlEncode for the token in the URL
                var encodedToken = WebUtility.UrlEncode(token);
                var confirmationLink = $"https://localhost:7257/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

                await _emailService.SendEmailAsync(user.Email, "Confirm your email",
                    $"Click <a href='{confirmationLink}'>here</a> to confirm your email.");

                _logger.LogInformation("Confirmation email sent to {Email}. Token generated: {Token}", user.Email, token);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send confirmation email to {Email}. Registration succeeded anyway.", user.Email);
                // Continue with registration even if email fails
            }

            return Result<string>.Ok("User registered successfully. Please check your email for confirmation.");
        }
    }
}