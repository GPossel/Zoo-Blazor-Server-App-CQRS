using Domain.Users;
using FluentAssertions;

namespace Tests.Domain.xUnitTests
{
    public class UserTests
    {
        [Fact]
        public void Create_Should_CreateUser_WhenNameIsValid()
        {
            var email = Email.Create("test@test.com").Value;
            var name = new Name("John", "Doe");

            var user = User.Create(email, name, true);

            user.Should().NotBeNull();
        }

        [Fact]
        public void Create_Should_RaiseDomainEvent_WhenNameIsValid()
        {
            var email = Email.Create("test@test.com").Value;
            var name = new Name("John", "Doe");

            var user = User.Create(email, name, true);

            user.DomainEvents
                .Should().ContainSingle()
                .Which
                .Should().BeOfType<UserCreatedDomainEvent>();
        }
    }
}