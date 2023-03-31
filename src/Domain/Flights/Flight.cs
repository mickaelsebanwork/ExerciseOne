using System;
using Ether.Outcomes;
using Exercise_1.Domain.SeedWork;
using MediatR;

namespace Exercise_1.Domain.Flights
{
    public sealed class Flight : Aggregate
    {
        private Flight()
        {
        }

        private Flight(string flightNumber, DateTime flightDate, Airport departureAirport, Airport arrivalAirport, Aircraft flightAircraft,
            double flightDistanceInKilometers, double totalFlightConsumption)
        {
            FlightNumber = flightNumber;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            FlightAircraft = flightAircraft;
            FlightDate = flightDate;
            FlightDistanceInKilometers = flightDistanceInKilometers;
            TotalFlightConsumption = totalFlightConsumption;
        }

        public static IOutcome<Flight> Create(string flightNumber, DateTime flightDate, Airport departureAirport, Airport arrivalAirport, Aircraft flightAircraft)
        {
            if (string.IsNullOrWhiteSpace(flightNumber))
            {
                return Outcomes
                    .Failure<Flight>()
                    .WithMessage("The flightNumber cannot be null or empty");
            }

            if (flightDate <= DateTime.Now.AddDays(1))
            {
                return Outcomes
                    .Failure<Flight>()
                    .WithMessage($"A flight should be create at least a day before the flightDate: {flightDate}");
            }

            var distanceInKilometer = departureAirport.Location.DistanceInKm(arrivalAirport.Location);
            var totalFlightConsumption = flightAircraft.AverageFuelConsumption.CalculateTotalFlightConsumption(distanceInKilometer);

            var flight = new Flight(flightNumber,
                flightDate,
                departureAirport,
                arrivalAirport,
                flightAircraft,
                distanceInKilometer,
                totalFlightConsumption);

            return Outcomes.Success(flight);
        }

        public string FlightNumber { get; }
        public Airport DepartureAirport { get; }
        public Airport ArrivalAirport { get; }
        public Aircraft FlightAircraft { get; }
        public DateTime FlightDate { get; private set; }
        public double FlightDistanceInKilometers { get; }
        public double TotalFlightConsumption { get; }

        public IOutcome DelayFlight(DateTime newFlightDate)
        {
            if (newFlightDate <= FlightDate)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"The new date: {newFlightDate} must be greater than the previous one: {FlightDate}");
            }

            const int minGapInHoursBetweenFlightDataAndNewFlightDate = 3;
            if (FlightDate.AddHours(minGapInHoursBetweenFlightDataAndNewFlightDate) >= newFlightDate)
            {
                return Outcomes.Failure().WithMessage(
                    $"The new date: {newFlightDate} must be greater than {minGapInHoursBetweenFlightDataAndNewFlightDate}hours " +
                    $"compared to the previous date: {FlightDate}");
            }

            FlightDate = newFlightDate;
            return Outcomes.Success();
        }
    }
}