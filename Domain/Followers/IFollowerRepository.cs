namespace Domain.Followers
{
    public interface IFollowerRepository
    {
        public Task<bool> IsAlreadyFollowingAsync(Guid currentUserId, Guid followingUserId, CancellationToken cancellationToken);
        public Task Insert(Follower follower, CancellationToken cancellationToken);
        public Task Update(Follower follower, CancellationToken cancellationToken);
    }
}
