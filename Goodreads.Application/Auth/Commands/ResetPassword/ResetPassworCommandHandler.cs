using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(
            UserManager<User> userManager,
            ILogger<ResetPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Resetting password for user ID: {UserId}", request.userId);

            var user = await _userManager.FindByIdAsync(request.userId);
            if (user == null)
            {
                return Result.Fail(UserErrors.NotFound(request.userId));
            }

            try
            {
                // URL DECODE the token before using it
                var decodedToken = WebUtility.UrlDecode(request.Token);

                _logger.LogInformation("Original token: {Token}", request.Token);
                _logger.LogInformation("Decoded token: {DecodedToken}", decodedToken);

                var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

                if (!result.Succeeded)
                {
                    _logger.LogError("Password reset failed. Errors: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return Result.Fail(AuthErrors.InvalidToken);
                }

                _logger.LogInformation("Password reset successful for user: {Email}", user.Email);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting password for user ID: {UserId}", request.userId);
                return Result.Fail(AuthErrors.InvalidToken);
            }
        }
    }
}
