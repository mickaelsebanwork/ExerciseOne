using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain.Flights;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal sealed class FlightRepositoryInMemory : IFlightRepository
    {
        private readonly FlightsDbContext _dbContext;

        public FlightRepositoryInMemory(FlightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IOutcome> Delete(Flight flight)
        {
            var entityEntry = _dbContext.Remove(flight);

            IOutcome outcome = entityEntry.State == EntityState.Deleted
                ? Outcomes.Success()
                : Outcomes.Failure();

            return await Task.FromResult(outcome);
        }

        public async Task<IOutcome<Flight>> Find(string flightNumber)
        {
            var flightLookup = await _dbContext.Flights
                .FirstOrDefaultAsync(x => x.FlightNumber == flightNumber);

            return flightLookup is not null
                ? Outcomes.Success(flightLookup)
                : Outcomes.Failure<Flight>();
        }

        public async Task<IOutcome> Add(Flight flight)
        {
            var entityEntry = await _dbContext.Flights.AddAsync(flight);

            return entityEntry.State == EntityState.Added
                ? Outcomes.Success()
                : Outcomes.Failure();
        }
    }
}