using SharedKernel;

namespace Application.Abstractions.Messaging
{
    public interface IQuery<TResponse>
    {
    }

    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handler(TQuery query, CancellationToken cancellationToken);
    }
}
