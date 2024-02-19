using Infrastructure.Entities.Followers;

namespace Domain.Followers
{
    public interface IFollowerRepository
    {
        Task<FollowerDbo?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<FollowerDbo?> GetByUserIdAndFollowerIdAsync(Guid userId, Guid followerId, CancellationToken cancellationToken);
        public Task<bool> IsAlreadyFollowingAsync(Guid currentUserId, Guid followingUserId, CancellationToken cancellationToken);
        public Task Create(Guid user, Guid follower, DateTime timeStamp, CancellationToken cancellationToken);
        public Task Insert(FollowerDbo follower, CancellationToken cancellationToken);
        public Task Update(FollowerDbo follower, CancellationToken cancellationToken);
        public Task Delete(FollowerDbo follower, CancellationToken cancellationToken);
    }
}