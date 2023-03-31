using Ether.Outcomes;
using MediatR;

namespace Exercise_1.Application.DeleteFlight
{
    public sealed class DeleteFlightCommand : IRequest<IOutcome>
    {
        public DeleteFlightCommand(string flightNumber)
        {
            FlightNumber = flightNumber;
        }

        public string FlightNumber { get; }
    }
}