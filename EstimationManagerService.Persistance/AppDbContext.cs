using EstimationManagerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

    }
}