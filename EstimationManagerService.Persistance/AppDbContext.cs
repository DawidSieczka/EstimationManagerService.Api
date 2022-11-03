using System.Reflection;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance.FluentApi;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskTimeDetails> TaskTimeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}