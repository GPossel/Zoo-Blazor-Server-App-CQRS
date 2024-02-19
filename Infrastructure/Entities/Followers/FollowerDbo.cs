using Infrastructure.Entities.Users;
using Infrastructure.Followers;
using SharedKernel;

namespace Infrastructure.Entities.Followers
{
    public sealed class FollowerDbo : Entity
    {
        public Guid UserId { get; private set; }
        public UserDbo User { get; set; } = null!;
        public Guid FollowedId { get; private set; }
        public UserDbo Followed { get; set; } = null!;
        public DateTime FollowedAt { get; set; }

        public FollowerDbo()
        {

        }

        public FollowerDbo(Guid userId, UserDbo user, Guid followedId, UserDbo followed, DateTime followedAt)
        {
            UserId = userId;
            User = user;
            FollowedId = followedId;
            Followed = followed;
            FollowedAt = followedAt;
        }

        public FollowerDbo(Guid id, Guid userId, Guid followerId, DateTime followedAt)
             : base(id)
        {
            UserId = userId;
            FollowedId = followerId;
            FollowedAt = followedAt;
        }

        public FollowerDbo(Guid id, UserDbo user, UserDbo followed, DateTime followedAt)
            : base(id)
        {
            User = user;
            Followed = followed;
            FollowedAt = followedAt;
        }

        public static FollowerDbo Create(Guid userId, Guid followerId, DateTime followedAt)
        {
            var follower = new FollowerDbo(
                Guid.NewGuid(),
                userId,
                followerId,
                followedAt);

            follower.Raise(new FollowerCreatedDomainEvent(follower.Id));
            return follower;
        }

        public static FollowerDbo Create(UserDbo userDbo, UserDbo followerDbo, DateTime followedAt)
        {
            var follower = new FollowerDbo(
                Guid.NewGuid(),
                userDbo,
                followerDbo,
                followedAt);

            follower.Raise(new FollowerCreatedDomainEvent(follower.Id));
            return follower;
        }
    }
}
