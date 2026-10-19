using Application.Interfaces;
using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UserRepository(AITeacherDbContext context) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellation)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellation);
    }

    public async Task AddAsync(User user, CancellationToken cancellation)
    {
        await context.Users.AddAsync(user, cancellation);
        await context.SaveChangesAsync(cancellation);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user, CancellationToken cancellation)
    {
        context.Update(user);
        await context.SaveChangesAsync(cancellation);
    }
}