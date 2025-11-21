using Goodreads.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.Auth.Commands.RefreshToken
{
    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResultDto>>
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(
            ITokenProvider tokenProvider,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<RefreshTokenCommandHandler> logger)
        {
            _tokenProvider = tokenProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        public async Task<Result<AuthResultDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // 1. البحث عن الـ refresh token
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            // 2. التحقق من الصلاحية - استخدام ExpiryDate كما في الـ Entity
            if (storedRefreshToken == null)
            {
                _logger.LogWarning("Refresh token not found: {Token}", request.RefreshToken);
                return Result<AuthResultDto>.Fail(Error.Failure("Auth.InvalidRefreshToken", "Invalid refresh token"));
            }

            if (storedRefreshToken.IsRevoked)
            {
                _logger.LogWarning("Revoked refresh token for user {UserId}", storedRefreshToken.UserId);
                return Result<AuthResultDto>.Fail(Error.Failure("Auth.RevokedRefreshToken", "Refresh token has been revoked"));
            }

            if (storedRefreshToken.IsUsed)
            {
                _logger.LogWarning("Already used refresh token for user {UserId}", storedRefreshToken.UserId);
                return Result<AuthResultDto>.Fail(Error.Failure("Auth.UsedRefreshToken", "Refresh token has already been used"));
            }

            if (storedRefreshToken.ExpiryDate < DateTime.UtcNow) // استخدام ExpiryDate
            {
                _logger.LogWarning("Expired refresh token for user {UserId}", storedRefreshToken.UserId);
                return Result<AuthResultDto>.Fail(Error.Failure("Auth.ExpiredRefreshToken", "Refresh token has expired"));
            }

            // 3. تحديث حالة الـ token
            storedRefreshToken.IsUsed = true;
            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            // 4. الحصول على المستخدم
            if (storedRefreshToken.User == null)
            {
                _logger.LogError("User not found for refresh token");
                return Result<AuthResultDto>.Fail(Error.Failure("Auth.UserNotFound", "User not found"));
            }

            // 5. إنشاء tokens جديدة
            var accessToken = await _tokenProvider.GenerateAccessTokenAsync(storedRefreshToken.User);
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            var newRefreshToken = await _tokenProvider.GenerateAndStoreRefreshTokenAsync(storedRefreshToken.User, jwtToken.Id);

            // 6. إرجاع النتيجة
            var authResult = new AuthResultDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };

            _logger.LogInformation("Tokens refreshed successfully for user {UserId}", storedRefreshToken.UserId);
            return Result<AuthResultDto>.Ok(authResult);
        }
    }
}
