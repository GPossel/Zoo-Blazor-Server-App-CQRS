using Domain.Users;
using SharedKernel;

namespace Domain.Followers
{
    public sealed class Follower : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid FollowedId { get; private set; }
        public User Followed { get; private set; }
        public DateTime FollowedAt { get; set; }

        public Follower(Guid id, Guid userId, Guid followerId, DateTime followedAt)
             : base(id)
        {
            UserId = userId;
            FollowedId = followerId;
            FollowedAt = followedAt;
        }

        public static Follower Create(Guid userId, Guid followerId, DateTime followedAt)
        {
            var follower = new Follower(
                Guid.NewGuid(),
                userId,
                followerId,
                followedAt);

            follower.Raise(new FollowerCreatedDomainEvent(follower.Id));
            return follower;
        }
    }
}
