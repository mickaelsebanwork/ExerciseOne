using System.Threading.Tasks;
using Ether.Outcomes;

namespace Exercise_1.Domain.Flights
{
    public interface IAirportGateway
    {
        Task<IOutcome<Airport>> Find(string code);
    }
}