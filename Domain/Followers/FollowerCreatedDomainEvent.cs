using SharedKernel;

namespace Domain.Followers
{
    public sealed record FollowerCreatedDomainEvent(Guid id) : IDomainEvent
    {
    }
}