using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goodreads.Application.UserFollows.Commands.UnFollowUser
{
    public class UnFollowUserCommandHandler : IRequestHandler<UnFollowUserCommand, Result>
    {
        private readonly ILogger<UnFollowUserCommandHandler> _logger;
        private readonly IUserFollowRepository _userFollowRepository;
        private readonly IUserContext _userContext;
        private readonly UserManager<User> _userManager;

        public UnFollowUserCommandHandler(ILogger<UnFollowUserCommandHandler> logger, IUserFollowRepository userFollowRepository, IUserContext userContext, UserManager<User> userManager)
        {
            _logger = logger;
            _userFollowRepository = userFollowRepository;
            _userContext = userContext;
            _userManager = userManager;
        }
        public async Task<Result> Handle(UnFollowUserCommand request, CancellationToken cancellationToken)
        {
            var followerId = _userContext.UserId;
            var followingId = request.FollowingId;
            _logger.LogInformation("User with Id : {FollowerId} is unfollowing user with Id : {FollowingId}", followerId, followingId);
            var follower = await _userManager.FindByIdAsync(followerId);
            if (follower == null)
                return Result.Fail(FollowErrors.UserNotFound(followerId));

            var following = await _userManager.FindByIdAsync(request.FollowingId);
            if (following is null)
                return Result.Fail(FollowErrors.UserNotFound(request.FollowingId));

            var isFollowing = await _userFollowRepository.IsFollowingAsync(followerId, followingId);

            if (!isFollowing)
                return Result.Fail(FollowErrors.NotFollowing(followerId, followingId));

            await _userFollowRepository.UnfollowAsync(followerId, followingId);

            return Result.Ok();
        }
    }
}
