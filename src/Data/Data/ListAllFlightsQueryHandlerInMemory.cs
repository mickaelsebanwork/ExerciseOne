using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exercise_1.Application.ListAllFlights;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal sealed class
        ListAllFlightsQueryHandlerInMemory : IRequestHandler<ListAllFlightsQuery, IReadOnlyList<FlightDto>>
    {
        private readonly FlightsDbContext _dbContext;

        public ListAllFlightsQueryHandlerInMemory(FlightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<FlightDto>> Handle(ListAllFlightsQuery request,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Flights
                .AsNoTracking()
                .Select(f =>
                    new FlightDto(
                        f.FlightNumber,
                        f.FlightDate,
                        f.FlightDistanceInKilometers,
                        f.TotalFlightConsumption,
                        f.FlightAircraft.InternalCode,
                        f.FlightAircraft.Brand,
                        f.FlightAircraft.Model,
                        f.FlightAircraft.AverageFuelConsumption.LitersPerKilometer,
                        f.FlightAircraft.AverageFuelConsumption.TakeoffEffort,
                        f.FlightAircraft.ManufactureDate,
                        f.DepartureAirport.Code,
                        f.DepartureAirport.Name,
                        f.DepartureAirport.Address,
                        f.DepartureAirport.City,
                        f.DepartureAirport.Country,
                        f.DepartureAirport.Location.Latitude,
                        f.DepartureAirport.Location.Longitude,
                        f.ArrivalAirport.Code,
                        f.ArrivalAirport.Name,
                        f.ArrivalAirport.Address,
                        f.ArrivalAirport.City,
                        f.ArrivalAirport.Country,
                        f.ArrivalAirport.Location.Latitude,
                        f.ArrivalAirport.Location.Longitude))
                .ToListAsync(cancellationToken);
        }
    }
}