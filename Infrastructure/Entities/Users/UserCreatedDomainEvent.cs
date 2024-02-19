using SharedKernel;

namespace Infrastructure.Users
{
    public sealed record UserCreatedDomainEvent(Guid id) : IDomainEvent
    {

    }
}