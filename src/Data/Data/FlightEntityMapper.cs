using Exercise_1.Domain.Flights;
using JsonNet.ContractResolvers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    internal class FlightEntityMapper : IEntityTypeConfiguration<Flight>
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            ContractResolver = new PrivateSetterContractResolver()
        };

        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber);
            builder.Property(x => x.FlightDate);
            builder.Property(x => x.FlightDistanceInKilometers);
            builder.Property(x => x.TotalFlightConsumption);

            builder.Property(x => x.ArrivalAirport)
                .HasConversion(
                    @object => JsonConvert.SerializeObject(@object),
                    json => JsonConvert.DeserializeObject<Airport>(json, Settings));

            builder.Property(x => x.DepartureAirport)
                .HasConversion(
                    @object => JsonConvert.SerializeObject(@object),
                    json => JsonConvert.DeserializeObject<Airport>(json, Settings));

            builder.Property(x => x.FlightAircraft)
                .HasConversion(
                    @object => JsonConvert.SerializeObject(@object),
                    json => JsonConvert.DeserializeObject<Aircraft>(json, Settings));
        }
    }
}