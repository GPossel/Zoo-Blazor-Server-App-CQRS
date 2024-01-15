namespace Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand commnd, CancellationToken cancellation);
    }
}