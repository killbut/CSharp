using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DataDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;
        public DataDbContext(DbContextOptions<DataDbContext> opt) : base(opt) 
        {
        }

        public DataDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new WorkerConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
        }
    }
}
