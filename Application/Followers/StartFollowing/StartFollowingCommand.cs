using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Followers;
using Domain.Users;
using Infrastructure.Entities.Users;
using SharedKernel;

namespace Application.Followers.StartFollowing
{
    public sealed record StartFollowingCommand(Guid UserId, Guid FollowedId) : ICommand;

    internal sealed class StartFollowingCommandHandler : ICommandHandler<StartFollowingCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly FollowerService _followerService;
        private readonly IUnitOfWork _unitOfWork;

        public StartFollowingCommandHandler(IUserRepository userRepository, FollowerService followerService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _followerService = followerService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(StartFollowingCommand command, CancellationToken cancellationToken)
        {
            UserDbo? user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
            if (user is null)
            {
                return UserErrors.NotFound(command.UserId);
            }

            UserDbo? followed = await _userRepository.GetByIdAsync(command.FollowedId, cancellationToken);
            if (followed is null)
            {
                return UserErrors.NotFound(command.FollowedId);
            }

            var result = await _followerService.StartFollowingAsync(user, followed, cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await _unitOfWork.SaveChangedAsync(cancellationToken);

            return Result.Success();
        }
    }

    public static class UserErrors
    {
        public static Error NotFound(Guid id) => new("User.NotFound", $"User not found with id = {id}.");
    }
}