using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AITeacherDbContext(
    DbContextOptions<AITeacherDbContext> options)
    : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<Class> Classes => Set<Class>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AITeacherDbContext).Assembly);
    }
}
