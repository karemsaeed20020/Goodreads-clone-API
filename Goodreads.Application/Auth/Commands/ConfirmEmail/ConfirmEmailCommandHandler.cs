using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.Confirm_Email
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result<bool>>
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return Result<bool>.Fail(UserErrors.NotFound(request.UserId));

            if (user.EmailConfirmed)
                return Result<bool>.Fail(UserErrors.EmailAlreadyConfirmed(request.UserId));

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
                return Result<bool>.Fail(AuthErrors.InvalidToken);

            return Result<bool>.Ok(true);
        }
    }
}
