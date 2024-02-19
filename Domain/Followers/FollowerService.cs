using Domain.Users;
using Infrastructure.Entities.Followers;
using Infrastructure.Entities.Users;
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
            UserDbo user,
            UserDbo followed,
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

            var follower = Infrastructure.Entities.Followers.FollowerDbo.Create(user.Id, followed.Id, _dateTimeProvider.UtcNow);

            await _followerRepository.Insert(follower, cancellationToken);

            return Result.Success();
        }

        public async Task<Result> RemoveFollowingAsync(Guid followingId, CancellationToken cancellationToken)
        {
            FollowerDbo? following = await _followerRepository.GetByIdAsync(followingId, cancellationToken);

            if (following is null)
            {
                return Result.Success();
            }

            await _followerRepository.Delete(following, cancellationToken);
            return Result.Success();
        }

        public async Task<FollowerDbo?> FindFollowingAsync(UserDbo user, UserDbo follower, CancellationToken cancellationToken)
        {
            FollowerDbo? following = await _followerRepository.GetByUserIdAndFollowerIdAsync(user.Id, follower.Id, cancellationToken);
            return following;
        }
    }
}