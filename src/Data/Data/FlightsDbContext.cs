using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Exercise_1.Domain.Flights;
using Exercise_1.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class FlightsDbContext : DbContext, IUnitOfWork
    {
        public FlightsDbContext(DbContextOptions<FlightsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }


        public Task Save(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(GetType()));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            }
        }
    }
}