using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Context
{
    public class DataDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Worker> Workers { get; set; } = null!;
        
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
            
        }
    }
}
