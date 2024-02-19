using Infrastructure.Entities.Users;

namespace Domain.Users
{
    public interface IUserRepository
    {
        Task<UserDbo?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserDbo>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<UserDbo>> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<IEnumerable<UserDbo>> GetByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}
