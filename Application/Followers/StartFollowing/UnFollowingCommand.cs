using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Followers;
using Domain.Users;
using Infrastructure.Entities.Followers;
using Infrastructure.Entities.Users;
using SharedKernel;

namespace Application.Followers.StartFollowing
{
    internal sealed record UnFollowingCommand(Guid userId, Guid followerId) : ICommand;

    internal sealed class UnFollowingCommandHandler : ICommandHandler<UnFollowingCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly FollowerService _followerService;
        private readonly IUnitOfWork _unitOfWork;

        public UnFollowingCommandHandler(IUserRepository userRepository, FollowerService followerService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _followerService = followerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnFollowingCommand command, CancellationToken cancellationToken)
        {
            UserDbo? user = await _userRepository.GetByIdAsync(command.userId, cancellationToken);

            if (user is null)
            {
                return UserErrors.NotFound(command.userId);
            }

            UserDbo? follower = await _userRepository.GetByIdAsync(command.followerId, cancellationToken);

            if (follower is null)
            {
                return UserErrors.NotFound(command.followerId);
            }

            FollowerDbo? following = await _followerService.FindFollowingAsync(user, follower, cancellationToken);

            if (following is null)
            {
                return FollowerErrors.FollowerNotFound(command.userId, command.followerId);
            }

            await _followerService.RemoveFollowingAsync(following.Id, cancellationToken);
            await _unitOfWork.SaveChangedAsync(cancellationToken);

            return Result.Success();
        }
    }
}
