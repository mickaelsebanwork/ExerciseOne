using System;
using System.Threading.Tasks;
using Exercise_1.Api.SeedWork;
using Exercise_1.Application.CreateFlight;
using Exercise_1.Application.DelayFlight;
using Exercise_1.Application.DeleteFlight;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_1.Api.Flights
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class FlightsController : BaseController
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{flightNumber}")]
        public async Task<IActionResult> Delete(string flightNumber)
        {
            return FromOutcome(await _mediator.Send(new DeleteFlightCommand(flightNumber)));
        }

        [HttpPut("{flightNumber}/delay")]
        public async Task<IActionResult> Delay(string flightNumber, [FromBody] DelayFlightDto dto)
        {
            return FromOutcome(await _mediator.Send(new DelayFlightCommand(flightNumber, dto.FlightNewDate)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFlightDto dto)
        {
            return FromOutcome(await _mediator.Send(new CreateFlightCommand(dto.AircraftInternalCode,
                dto.ArrivalAirportCode,
                dto.DepartureAirportCode,
                dto.FlightDate,
                dto.FlightNumber)));
        }
    }
}