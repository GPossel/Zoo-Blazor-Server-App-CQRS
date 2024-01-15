using SharedKernel;

namespace Application.Abstractions.Messaging
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<Result> Handle(TCommand commnd, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { 
        Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    }
}