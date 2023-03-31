using System.Threading.Tasks;
using Ether.Outcomes;

namespace Exercise_1.Domain.Flights
{
    public interface IAircraftGateway
    {
        Task<IOutcome<Aircraft>> Find(string internalCode);
    }
}