using System.Threading;
using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain.Flights;
using Exercise_1.Domain.SeedWork;
using MediatR;

namespace Exercise_1.Application.CreateFlight
{
    internal sealed class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, IOutcome>
    {
        private readonly IAircraftGateway _aircraftGateway;
        private readonly IAirportGateway _airportGateway;
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFlightCommandHandler(IAircraftGateway aircraftGateway, IAirportGateway airportGateway,
            IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _aircraftGateway = aircraftGateway;
            _airportGateway = airportGateway;
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IOutcome> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.Find(request.FlightNumber);
            if (flight.Success)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"A flight with flightNumber {request.FlightNumber} already exists");
            }

            var aircraft = await _aircraftGateway.Find(request.AircraftInternalCode);
            if (aircraft.Failure)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"No aircraft found for AircraftInternalCode: {request.AircraftInternalCode}");
            }

            var departureAirport = await _airportGateway.Find(request.DepartureAirportCode);
            if (departureAirport.Failure)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"No airport found for Code: {request.DepartureAirportCode}");
            }

            var arrivalAirport = await _airportGateway.Find(request.ArrivalAirportCode);
            if (arrivalAirport.Failure)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"No airport found for Code {request.ArrivalAirportCode}");
            }

            var flightOutcome = Flight.Create(request.FlightNumber,
                request.FlightDate,
                departureAirport.Value,
                arrivalAirport.Value,
                aircraft.Value);

            if (flightOutcome.Failure)
            {
                return flightOutcome;
            }

            await _flightRepository.Add(flightOutcome.Value);
            await _unitOfWork.Save(cancellationToken);
            return Outcomes.Success();
        }
    }
}