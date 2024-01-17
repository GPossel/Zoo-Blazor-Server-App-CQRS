using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Domain.xUnitTests.FollowerTests
{
    public class FollowerServiceTests
    {
        private readonly FollowerService _followerService;
        private readonly IFollowerRepository _followerRepositoryMock;
        private static readonly Email Email = Email.Create("test@test.com").Value;
        private static readonly Name Name = new("John", "Doe");
        private static readonly DateTime UtcNow = DateTime.UtcNow;

        public FollowerServiceTests()
        {
            _followerRepositoryMock = Substitute.For<IFollowerRepository>();
            var dateTimeProvider = Substitute.For<IDateTimeProvider>();
            dateTimeProvider.UtcNow.Returns(UtcNow);
            _followerService = new FollowerService(_followerRepositoryMock, dateTimeProvider);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
        {
            var user = User.Create(Email, Name, hasPublicProfile: false);

            var result = await _followerService.StartFollowingAsync(user, user, default);

            result.Error.Should().Be(FollowerErrors.SameUser);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnError_WhenProfileNonPublic()
        {
            var user = User.Create(Email, Name, hasPublicProfile: false);
            var followed = User.Create(Email, Name, hasPublicProfile: false);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.Error.Should().Be(FollowerErrors.NonPublicProfile);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnError_WhenIsAlreadyFollowing()
        {
            var user = User.Create(Email, Name, hasPublicProfile: true);
            var followed = User.Create(Email, Name, hasPublicProfile: true);

            _followerRepositoryMock
                .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
                .Returns(true);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.Error.Should().Be(FollowerErrors.AlreadyFollowing);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnSuccess()
        {
            var user = User.Create(Email, Name, hasPublicProfile: true);
            var followed = User.Create(Email, Name, hasPublicProfile: true);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_CallInsertOnRepository_WhenFollowerCreated()
        {
            var user = User.Create(Email, Name, hasPublicProfile: true);
            var followed = User.Create(Email, Name, hasPublicProfile: true);

            _followerRepositoryMock
                .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
                .Returns(false);

            await _followerService.StartFollowingAsync(user, followed, default);

            await _followerRepositoryMock.Received(1)
                .Insert(Arg.Is<Follower>(f => f.UserId == user.Id &&
                                              f.FollowedId == followed.Id &&
                                              f.FollowedAt == UtcNow), default);
        }

        [Fact]
        public async Task UnFollowingAsync_Should_ReturnError_WhenUserNotFound() { }

        [Fact]
        public async Task UnFollowingAsync_Should_ReturnError_WhenFollowerNotFound() { }

        [Fact]
        public async Task UnFollowingAsync_Should_ReturnError_WhenFollowingNotExisting() { }

        [Fact]
        public async Task UnFollowingAsync_Should_ReturnSuccess() { }

        [Fact]
        public async Task UnFollowingAsync_Should_CallDeleteOnRepository_WhenUnFollowing()
        {
            var user = User.Create(Email, Name, hasPublicProfile: true);
            var followed = User.Create(Email, Name, hasPublicProfile: true);
            var follower = Follower.Create(user.Id, followed.Id, UtcNow);

            _followerRepositoryMock
                .GetByUserIdAndFollowerIdAsync(user.Id, followed.Id, default)
                .Returns(follower);

            await _followerRepositoryMock.Received(1)
                    .Delete(Arg.Is<Follower>(f => f.UserId == user.Id &&
                                                  f.FollowedId == followed.Id &&
                                                  f.FollowedAt == UtcNow), default);
        }

    }
}
