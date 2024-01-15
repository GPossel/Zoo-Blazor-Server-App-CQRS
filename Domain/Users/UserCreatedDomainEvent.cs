using SharedKernel;

namespace Domain.Users
{
    public sealed record UserCreatedDomainEvent(Guid id) : IDomainEvent
    {

    }
}