using System;
using Ether.Outcomes;
using Exercise_1.Domain.SeedWork;

namespace Exercise_1.Domain.Flights
{
    public sealed class AverageFuelConsumption : ValueObject<AverageFuelConsumption>
    {
        private AverageFuelConsumption()
        {
        }

        private AverageFuelConsumption(double litersPerKilometer, double takeoffEffort)
        {
            LitersPerKilometer = litersPerKilometer;
            TakeoffEffort = takeoffEffort;
        }

        public double LitersPerKilometer { get; private set; }

        public double TakeoffEffort { get; private set; }

        public static IOutcome< AverageFuelConsumption> Create(double litersPerKilometer, double takeoffEffort)
        {
            if (litersPerKilometer < 0)
            {
                return Outcomes
                    .Failure<AverageFuelConsumption>().WithMessage(
                    "Average fuel consumption cannot be negative.");
            }

            if (takeoffEffort < 0)
            {
                return Outcomes
                    .Failure<AverageFuelConsumption>().WithMessage("Takeoff effort cannot be negative.");
            }

            return Outcomes.Success(new AverageFuelConsumption(litersPerKilometer, takeoffEffort));
        }

        public double CalculateTotalFlightConsumption(double flightDistanceInKilometer)
        {
            return flightDistanceInKilometer * LitersPerKilometer + TakeoffEffort;
        }

        protected override bool EqualsCore(AverageFuelConsumption other)
        {
            return Math.Abs(LitersPerKilometer - other.LitersPerKilometer) < 0.0001 &&
                   Math.Abs(TakeoffEffort - other.TakeoffEffort) < 0.0001;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(LitersPerKilometer, TakeoffEffort);
        }
    }
}