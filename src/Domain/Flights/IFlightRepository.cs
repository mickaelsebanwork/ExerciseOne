using System;
using System.Threading.Tasks;
using Ether.Outcomes;

namespace Exercise_1.Domain.Flights
{
    public interface IFlightRepository
    {
        Task<IOutcome> Delete(Flight flight);

        Task<IOutcome<Flight>> Find(string flightNumber);

        Task<IOutcome> Add(Flight flight);
    }
}