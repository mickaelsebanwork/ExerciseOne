using System.Collections.Generic;
using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain.Flights;

namespace Infrastructure.Gateway
{
    internal sealed class AirportFakeGateway : IAirportGateway
    {
        private static readonly Dictionary<string, Airport> Airports;

        static AirportFakeGateway()
        {
            Airports =  new Dictionary<string, Airport>
            {
                { "LIS", new Airport("LIS", "Lisbon Portela Airport", "Alameda das Comunidades Portuguesas, 1700-111 Lisboa, Portugal", "Lisbon", "Portugal", CoordinatePoints.Create("38.774167", "-9.134167").Value) },
                { "OPO", new Airport("OPO", "Francisco Sá Carneiro Airport", "Pedras Rubras, 4470-558 Maia, Portugal", "Porto", "Portugal", CoordinatePoints.Create("41.237947", "-8.671633").Value) },
                { "LHR", new Airport("LHR", "Heathrow Airport", "Longford, Hounslow TW6, UK", "London", "United Kingdom", CoordinatePoints.Create("51.470025", "-0.454295").Value) },
                { "CDG", new Airport("CDG", "Charles de Gaulle Airport", "95700 Roissy-en-France, France", "Paris", "France", CoordinatePoints.Create("49.009722", "2.547778").Value) },
                { "FRA", new Airport("FRA", "Frankfurt Airport", "60547 Frankfurt am Main, Germany", "Frankfurt", "Germany", CoordinatePoints.Create("50.033333", "8.570556").Value) },
                { "AMS", new Airport("AMS", "Amsterdam Airport Schiphol", "Evert van de Beekstraat 202, 1118 CP Schiphol, Netherlands", "Amsterdam", "Netherlands", CoordinatePoints.Create("52.308056", "4.764167").Value) },
                { "BCN", new Airport("BCN", "Barcelona–El Prat Airport", "08820 El Prat de Llobregat, Barcelona, Spain", "Barcelona", "Spain", CoordinatePoints.Create("41.296944", "2.078333").Value) },
                { "MUC", new Airport("MUC", "Munich Airport", "Nordallee 25, 85356 München-Flughafen, Germany", "Munich", "Germany", CoordinatePoints.Create("48.353783", "11.774614").Value) },
                { "FCO", new Airport("FCO", "Leonardo da Vinci–Fiumicino Airport", "Via dell'Aeroporto di Fiumicino, 320, 00054 Fiumicino RM, Italy", "Rome", "Italy", CoordinatePoints.Create("41.799167", "12.246389").Value) },
                { "ATH", new Airport("ATH", "Eleftherios Venizelos International Airport", "190 19 Spata Artemida, Greece", "Athens", "Greece", CoordinatePoints.Create("37.936389", "23.944444").Value) }
            };
        }

        public async Task<IOutcome<Airport>> Find(string code)
        {
            var hasValue = Airports.TryGetValue(code, out var lookupAirport);

            IOutcome<Airport> outcome = hasValue
                ? Outcomes.Success(lookupAirport)
                : Outcomes.Failure<Airport>();

            return await Task.FromResult(outcome);
        }
    }
}