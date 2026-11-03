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
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<Planning> Plannings => Set<Planning>();

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

        modelBuilder.Entity<JournalEntry>()
            .HasOne(j => j.Class)
            .WithMany()
            .HasForeignKey(j => j.SchoolClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<JournalEntry>()
            .HasOne(j => j.Planning)
            .WithMany()
            .HasForeignKey(j => j.PlanningId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AITeacherDbContext).Assembly);
    }
}