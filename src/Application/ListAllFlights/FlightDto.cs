using System;

namespace Exercise_1.Application.ListAllFlights
{
    public record FlightDto(
        string FlightNumber,
        DateTime FlightDate,
        double FlightDistanceInKilometers,
        double TotalFlightConsumption,
        string AircraftInternalCode,
        string AircraftBrand,
        string AircraftModel,
        double AircraftAverageConsumptionLitersPerKilometer,
        double AircraftAverageConsumptionTakeoffEffort,
        DateTime AircraftManufactureDate,
        string DepartureAirportCode,
        string DepartureAirportName,
        string DepartureAirportAddress,
        string DepartureAirportCity,
        string DepartureAirportCountry,
        double DepartureAirportLatitude,
        double DepartureAirportLongitude,
        string ArrivalAirportCode,
        string ArrivalAirportName,
        string ArrivalAirportAddress,
        string ArrivalAirportCity,
        string ArrivalAirportCountry,
        double ArrivalAirportLatitude,
        double ArrivalAirportLongitude
    );
}