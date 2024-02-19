using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using Infrastructure.Entities.Users;
using Infrastructure.Entities.Followers;
using NSubstitute;
using SharedKernel;
using Infrastructure.Users;

namespace Domain.xUnitTests.FollowerTests
{
    public class FollowerServiceTests
    {
        private readonly FollowerService _followerService;
        private readonly IFollowerRepository _followerRepositoryMock;
        private static readonly EmailDbo Email = EmailDbo.Create("test@test.com").Value;
        private static readonly NameDbo Name = NameDbo.Create("John", "Doe");
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
            var user = UserDbo.Create(Email, Name, hasPublicProfile: false);

            var result = await _followerService.StartFollowingAsync(user, user, default);

            result.Error.Should().Be(FollowerErrors.SameUser);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnError_WhenProfileNonPublic()
        {
            var user = UserDbo.Create(Email, Name, hasPublicProfile: false);
            var followed = UserDbo.Create(Email, Name, hasPublicProfile: false);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.Error.Should().Be(FollowerErrors.NonPublicProfile);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnError_WhenIsAlreadyFollowing()
        {
            var user = UserDbo.Create(Email, Name, hasPublicProfile: true);
            var followed = UserDbo.Create(Email, Name, hasPublicProfile: true);

            _followerRepositoryMock
                .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
                .Returns(true);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.Error.Should().Be(FollowerErrors.AlreadyFollowing);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_ReturnSuccess()
        {
            var user = UserDbo.Create(Email, Name, hasPublicProfile: true);
            var followed = UserDbo.Create(Email, Name, hasPublicProfile: true);

            var result = await _followerService.StartFollowingAsync(user, followed, default);

            result.IsSuccess.Should().Be(true);
        }

        [Fact]
        public async Task StartFollowingAsync_Should_CallInsertOnRepository_WhenFollowerCreated()
        {
            var user = UserDbo.Create(Email, Name, hasPublicProfile: true);
            var followed = UserDbo.Create(Email, Name, hasPublicProfile: true);

            _followerRepositoryMock
                .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
                .Returns(false);

            await _followerService.StartFollowingAsync(user, followed, default);

            await _followerRepositoryMock.Received(1)
                .Insert(Arg.Is<FollowerDbo>(f => f.UserId == user.Id &&
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
            var user = UserDbo.Create(Email, Name, hasPublicProfile: true);
            var followed = UserDbo.Create(Email, Name, hasPublicProfile: true);
            var follower = FollowerDbo.Create(user.Id, followed.Id, UtcNow);

            _followerRepositoryMock
                .GetByUserIdAndFollowerIdAsync(user.Id, followed.Id, default)
                .Returns(follower);

            await _followerRepositoryMock.Received(1)
                    .Delete(Arg.Is<FollowerDbo>(f => f.UserId == user.Id &&
                                                  f.FollowedId == followed.Id &&
                                                  f.FollowedAt == UtcNow), default);
        }

    }
}
