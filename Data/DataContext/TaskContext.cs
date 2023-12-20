using BrantMonitorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BrantMonitorApi.DataContext;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<TaskModel> TaskModel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskModel>()
            .HasIndex(t => new { t.Id })
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=Task;User Id=postgres;Password=root");
    }
}