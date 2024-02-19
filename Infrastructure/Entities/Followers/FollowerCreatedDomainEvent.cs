using SharedKernel;

namespace Infrastructure.Followers
{
    public sealed record FollowerCreatedDomainEvent(Guid id) : IDomainEvent
    {
    }
}