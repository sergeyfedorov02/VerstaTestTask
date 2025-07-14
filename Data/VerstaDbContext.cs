using Microsoft.EntityFrameworkCore;
using VerstaTestTask.Models;

namespace VerstaTestTask.Data
{
    public partial class VerstaDbContext : DbContext
    {
        public VerstaDbContext()
        {
        }

        public virtual DbSet<City> Cities { get; set; } = default!;
        public virtual DbSet<Order> Orders { get; set; } = default!;

        public VerstaDbContext(DbContextOptions<VerstaDbContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            OnModelBuilding(builder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}