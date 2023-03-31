using System.Threading;
using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain;
using Exercise_1.Domain.Flights;
using Exercise_1.Domain.SeedWork;
using MediatR;

namespace Exercise_1.Application.DeleteFlight
{
    internal sealed class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, IOutcome>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFlightCommandHandler(IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IOutcome> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.Find(request.FlightNumber);

            if (flight == null)
            {
                return Outcomes.Failure().WithMessage($"No flight found for Id {request.FlightNumber}");
            }

            var outcome = await _flightRepository.Delete(flight.Value);
            if (outcome.Failure)
            {
                return outcome;
            }

            await _unitOfWork.Save(cancellationToken);
            return Outcomes.Success();
        }
    }
}