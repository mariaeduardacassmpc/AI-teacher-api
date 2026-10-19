using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellation);
    Task AddAsync(User user, CancellationToken cancellation);
    Task UpdateAsync(User user, CancellationToken cancellation);
    Task SaveChangesAsync();
}