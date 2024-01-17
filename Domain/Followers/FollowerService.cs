using Domain.Users;
using SharedKernel;

namespace Domain.Followers
{
    public sealed class FollowerService
    {
        private readonly IFollowerRepository _followerRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FollowerService(IFollowerRepository followerRepository, IDateTimeProvider dateTimeProvider)
        {
            _followerRepository = followerRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> StartFollowingAsync(
            User user,
            User followed,
            CancellationToken cancellationToken)
        {
            if (user.Id == followed.Id)
            {
                return FollowerErrors.SameUser;
            }

            if (!followed.HasPublicProfile)
            {
                return FollowerErrors.NonPublicProfile;
            }

            if (await _followerRepository.IsAlreadyFollowingAsync(user.Id, followed.Id, cancellationToken))
            {
                return FollowerErrors.AlreadyFollowing;
            }

            var follower = Follower.Create(user.Id, followed.Id, _dateTimeProvider.UtcNow);

            await _followerRepository.Insert(follower, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> RemoveFollowingAsync(Guid followingId, CancellationToken cancellationToken)
        {
            Follower? following = await _followerRepository.GetByIdAsync(followingId, cancellationToken);

            if (following is null)
            {
                return Result.Success();
            }

            await _followerRepository.Delete(following, cancellationToken);
            return Result.Success();
        }

        public async Task<Follower?> FindFollowingAsync(User user, User follower, CancellationToken cancellationToken)
        {
            Follower? following = await _followerRepository.GetByUserIdAndFollowerIdAsync(user.Id, follower.Id, cancellationToken);
            return following;
        }
    }
}