using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AITeacherDbContext(
    DbContextOptions<AITeacherDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Material> Materials => Set<Material>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Class>()
            .HasMany(c => c.Subjects)
            .WithMany(s => s.Classes);

        modelBuilder.Entity<Material>()
            .HasMany(m => m.Classes)
            .WithMany(c => c.Materials);

        modelBuilder.Entity<Material>()
            .HasOne(m => m.Subject)
            .WithMany(s => s.Materials)
            .HasForeignKey(m => m.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AITeacherDbContext).Assembly);
    }
}