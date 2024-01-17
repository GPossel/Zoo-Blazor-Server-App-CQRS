namespace Domain.Followers
{
    public interface IFollowerRepository
    {
        Task<Follower?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Follower?> GetByUserIdAndFollowerIdAsync(Guid userId, Guid followerId, CancellationToken cancellationToken);
        public Task<bool> IsAlreadyFollowingAsync(Guid currentUserId, Guid followingUserId, CancellationToken cancellationToken);
        public Task Insert(Follower follower, CancellationToken cancellationToken);
        public Task Update(Follower follower, CancellationToken cancellationToken);
        public Task Delete(Follower follower, CancellationToken cancellationToken);
    }
}