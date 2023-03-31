using Ether.Outcomes;
using Microsoft.AspNetCore.Mvc;

namespace Exercise_1.Api.SeedWork
{
    public abstract class BaseController : Controller
    {
        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }

        protected IActionResult FromOutcome(IOutcome result)
        {
            return result.Success ? Ok() : Error(result.ToMultiLine(", "));
        }

        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }
    }
}