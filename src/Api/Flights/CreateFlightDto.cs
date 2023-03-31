using System;

namespace Exercise_1.Api.Flights
{
    public record CreateFlightDto(string AircraftInternalCode, string ArrivalAirportCode,
        string DepartureAirportCode, DateTime FlightDate, string FlightNumber);
}