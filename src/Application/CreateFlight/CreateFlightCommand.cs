using System;
using Ether.Outcomes;
using MediatR;

namespace Exercise_1.Application.CreateFlight
{
    public sealed class CreateFlightCommand : IRequest<IOutcome>
    {
        public CreateFlightCommand(string aircraftInternalCode, string arrivalAirportCode, string departureAirportCode,
            DateTime flightDate, string flightNumber)
        {
            AircraftInternalCode = aircraftInternalCode;
            ArrivalAirportCode = arrivalAirportCode;
            DepartureAirportCode = departureAirportCode;
            FlightDate = flightDate;
            FlightNumber = flightNumber;
        }

        public string AircraftInternalCode { get; }
        public string ArrivalAirportCode { get; }
        public string DepartureAirportCode { get; }
        public DateTime FlightDate { get; }
        public string FlightNumber { get; }
    }
}