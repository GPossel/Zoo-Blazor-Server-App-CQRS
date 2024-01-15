namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<User>> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
