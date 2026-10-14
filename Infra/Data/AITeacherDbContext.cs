using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AITeacherDbContext(
    DbContextOptions<AITeacherDbContext> options)
    : DbContext(options)
{

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
