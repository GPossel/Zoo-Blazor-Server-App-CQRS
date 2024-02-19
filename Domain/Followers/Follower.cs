using Domain.Users;

namespace Domain.Followers
{
    public class Follower
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid FollowedId { get; private set; }
        public User Followed { get; private set; }
        public DateTime FollowedAt { get; set; }
    }
}
