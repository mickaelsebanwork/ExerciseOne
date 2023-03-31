using System.Threading.Tasks;
using Exercise_1.Api.SeedWork;
using Exercise_1.Application.ListAllFlights;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_1.Api.FlightsReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsReportController : BaseController
    {
        private readonly IMediator _mediator;

        public FlightsReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new ListAllFlightsQuery()));
        }
    }
}