using SharedKernel;

namespace Domain.Followers
{
    public static class FollowerErrors
    {
        public static readonly Error SameUser = new("Follower.SameUser", "Follower is the same as the user.");
        public static readonly Error NonPublicProfile = new("Follower.NonPublicProfile", "Follower is not a public profile.");
        public static readonly Error AlreadyFollowing = new("Follower.AlreadyFollowing", "User already follows this account");
        public static Error FollowerNotFound(Guid userId, Guid followerId) => new("Follower.NotFound", $"Follower already unfollows this account. User {userId} is not following user {followerId}.");
    }
}
