using FluentAssertions;
using Infrastructure.Entities.Users;
using Infrastructure.Users;

namespace Tests.Domain.xUnitTests
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_CreateUser_WhenNameIsValid()
        {
            var email = EmailDbo.Create("test@test.com").Value;
            var name = NameDbo.Create("John", "Doe");

            var user = UserDbo.Create(email, name, true);

            user.Should().NotBeNull();
        }

        [Fact]
        public void Create_Should_RaiseDomainEvent_WhenNameIsValid()
        {
            var email = EmailDbo.Create("test@test.com").Value;
            var name = NameDbo.Create("John", "Doe");

            var user = UserDbo.Create(email, name, true);

            user.DomainEvents
                .Should().ContainSingle()
                .Which
                .Should().BeOfType<UserCreatedDomainEvent>();
        }
    }
}