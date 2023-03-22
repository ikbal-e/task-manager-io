using Microsoft.EntityFrameworkCore;
using TaskManagerIO.API.Entities;
using TaskManagerIO.API.Infrastructure.Data.EntityTypeConfigurations;

namespace TaskManagerIO.API.Infrastructure.Data;

public class TaskManagerContext : DbContext
{
    public TaskManagerContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobTypeConfiguration).Assembly);
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }
}
