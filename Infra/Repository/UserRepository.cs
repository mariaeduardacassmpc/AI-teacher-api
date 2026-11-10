using Application.Interfaces;
using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UserRepository(AITeacherDbContext context) : IUserRepository
{
    public async Task<bool> EmailExistsAsync(string email, int? userId, CancellationToken ct)
    {
        return await context.Users.AnyAsync(
            u => u.Email == email && 
                (userId == null || u.UserId != userId), ct);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
    {
        return await context.Users
          .AsNoTracking()
          .ToListAsync(ct);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Users.FindAsync([id], ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await context.Users.AddAsync(user, ct);
    }

    public Task UpdateAsync(User user, CancellationToken ct)
    {
        context.Users.Update(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(User user, CancellationToken ct)
    {
        context.Users.Remove(user);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await context.SaveChangesAsync(ct);
    }
}