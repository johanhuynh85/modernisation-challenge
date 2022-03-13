using Microsoft.EntityFrameworkCore;
using ModernisationChallenge.Domain.SeedWork;
using ModernisationChallenge.Domain.TaskAggregate;
using ModernisationChallenge.Infrastructure.EntityConfigurations;

namespace ModernisationChallenge.Infrastructure
{
    public class ModernisationDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ModernisationChallenge";

        public DbSet<TaskEntity> Tasks { get; set; }

        public ModernisationDbContext(DbContextOptions<ModernisationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskEntityTypeConfiguration());
        }
    }
}
