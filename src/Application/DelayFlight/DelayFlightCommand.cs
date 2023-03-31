using System;
using Ether.Outcomes;
using MediatR;

namespace Exercise_1.Application.DelayFlight
{
    public sealed class DelayFlightCommand : IRequest<IOutcome>
    {
        public DelayFlightCommand(string flightNumber, DateTime flightNewDate)
        {
            FlightNumber = flightNumber;
            FlightNewDate = flightNewDate;
        }

        public string FlightNumber { get; }
        public DateTime FlightNewDate { get; }
    }
}