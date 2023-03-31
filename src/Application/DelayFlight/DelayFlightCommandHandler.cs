using System.Threading;
using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain;
using Exercise_1.Domain.Flights;
using Exercise_1.Domain.SeedWork;
using MediatR;

namespace Exercise_1.Application.DelayFlight
{
    internal sealed class DelayFlightCommandHandler : IRequestHandler<DelayFlightCommand, IOutcome>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DelayFlightCommandHandler(IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IOutcome> Handle(DelayFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.Find(request.FlightNumber);

            if (flight.Failure)
            {
                return Outcomes
                    .Failure()
                    .WithMessage($"No flight found for flight number {request.FlightNumber}");
            }

            var outcome = flight.Value.DelayFlight(request.FlightNewDate);
            if (outcome.Failure)
            {
                return outcome;
            }

            await _unitOfWork.Save(cancellationToken);
            return Outcomes.Success();
        }
    }
}