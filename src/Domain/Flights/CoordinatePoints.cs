using System;
using Ether.Outcomes;
using Exercise_1.Domain.SeedWork;

namespace Exercise_1.Domain.Flights
{
    public class CoordinatePoints : ValueObject<CoordinatePoints>
    {
        private CoordinatePoints()
        {
        }

        private CoordinatePoints(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public static IOutcome<CoordinatePoints> Create(string latitude, string longitude)
        {
            if (string.IsNullOrEmpty(latitude))
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage("Latitude cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(longitude))
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage("Longitude cannot be null or empty.");
            }

            if (!double.TryParse(latitude, out var lat))
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage("Latitude must be a valid number.");
            }

            if (!double.TryParse(longitude, out var lon))
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage("Longitude must be a valid number.");
            }

            const double minLatitude = -90.0;
            const double maxLatitude = 90.0;
            const double minLongitude = -180.0;
            const double maxLongitude = 180.0;
            if (lat is < minLatitude or > maxLatitude)
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage(
                        $"Latitude must be between {minLatitude} and {maxLatitude}.");
            }

            if (lon is < minLongitude or > maxLongitude)
            {
                return Outcomes
                    .Failure<CoordinatePoints>().WithMessage(
                        $"Longitude must be between {minLongitude} and {maxLongitude}.");
            }

            return Outcomes.Success(new CoordinatePoints(lat, lon));
        }

        public double DistanceInKm(CoordinatePoints other)
        {
            const double earthRadiusKm = 6371.0; // Approximate radius of the Earth in kilometers

            var centralAngle = HaversineFormula(this, other);
            var distanceInKm = earthRadiusKm * centralAngle;

            return distanceInKm;
        }

        private static double HaversineFormula(CoordinatePoints coordinatePointsOne,
            CoordinatePoints coordinatePointsTwo)
        {
            var lat1Radians = ToRadians(coordinatePointsOne.Latitude);
            var lon1Radians = ToRadians(coordinatePointsOne.Longitude);
            var lat2Radians = ToRadians(coordinatePointsTwo.Latitude);
            var lon2Radians = ToRadians(coordinatePointsTwo.Longitude);

            // CalculateTotalFlightConsumption the square of half the chord length between the two points
            var halfChordLengthSquared =
                Math.Sin((lat2Radians - lat1Radians) / 2.0) * Math.Sin((lat2Radians - lat1Radians) / 2.0) +
                Math.Cos(lat1Radians) * Math.Cos(lat2Radians) *
                Math.Sin((lon2Radians - lon1Radians) / 2.0) * Math.Sin((lon2Radians - lon1Radians) / 2.0);

            // CalculateTotalFlightConsumption the central angle between the two points on the surface of the sphere
            var centralAngle =
                2.0 * Math.Atan2(Math.Sqrt(halfChordLengthSquared), Math.Sqrt(1.0 - halfChordLengthSquared));

            return centralAngle;
        }

        private static double ToRadians(double degrees)
        {
            // Conversion factor from degrees to radians
            const double degreesToRadians = Math.PI / 180.0;

            return degrees * degreesToRadians;
        }

        protected override bool EqualsCore(CoordinatePoints other)
        {
            const double tolerance = 0.000001;
            return Math.Abs(Latitude - other.Latitude) < tolerance && Math.Abs(Longitude - other.Longitude) < tolerance;
        }

        protected override int GetHashCodeCore()
        {
            return HashCode.Combine(Latitude, Longitude);
        }
    }
}