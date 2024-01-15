namespace Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangedAsync(CancellationToken cancellationToken = default);
    }
}
