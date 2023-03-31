using System.Collections.Generic;
using MediatR;

namespace Exercise_1.Application.ListAllFlights
{
    public sealed class ListAllFlightsQuery : IRequest<IReadOnlyList<FlightDto>>
    {
    }
}