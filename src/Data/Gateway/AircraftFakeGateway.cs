using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ether.Outcomes;
using Exercise_1.Domain.Flights;

namespace Infrastructure.Gateway
{
    internal sealed class AircraftFakeGateway : IAircraftGateway
    {
        private static readonly Dictionary<string, Aircraft> Aircrafts;

        static AircraftFakeGateway()
        {
            Aircrafts = new Dictionary<string, Aircraft>
            {
                { "AC001", new Aircraft("AC001", "Boeing", "747", AverageFuelConsumption.Create(3.5, 10).Value, new DateTime(2000, 1, 1)) },
                { "AC002", new Aircraft("AC002", "Airbus", "A380", AverageFuelConsumption.Create(3.2, 8).Value, new DateTime(2005, 6, 15)) },
                { "AC003", new Aircraft("AC003", "Embraer", "E190", AverageFuelConsumption.Create(2.1, 6).Value, new DateTime(2010, 9, 30)) },
                { "AC004", new Aircraft("AC004", "Cessna", "172", AverageFuelConsumption.Create(1.7, 4).Value, new DateTime(1999, 12, 25)) },
                { "AC005", new Aircraft("AC005", "Bombardier", "Global 6000", AverageFuelConsumption.Create(3.8, 12).Value, new DateTime(2015, 3, 1)) },
                { "AC006", new Aircraft("AC006", "Gulfstream", "G650", AverageFuelConsumption.Create(4.2, 14).Value, new DateTime(2018, 8, 10)) },
                { "AC007", new Aircraft("AC007", "Dassault", "Falcon 7X", AverageFuelConsumption.Create(3.6, 10).Value, new DateTime(2007, 4, 20)) },
                { "AC008", new Aircraft("AC008", "Boeing", "777", AverageFuelConsumption.Create(3.4, 10).Value, new DateTime(2012, 11, 5)) },
                { "AC009", new Aircraft("AC009", "Airbus", "A320", AverageFuelConsumption.Create(2.8, 6).Value, new DateTime(2002, 7, 7)) },
                { "AC010", new Aircraft("AC010", "Embraer", "Phenom 300", AverageFuelConsumption.Create(2.3, 4).Value, new DateTime(2017, 2, 14)) }
            };
        }

        public async Task<IOutcome<Aircraft>> Find(string internalCode)
        {
            var hasValue = Aircrafts.TryGetValue(internalCode, out var lookupAircraft);

            IOutcome<Aircraft> outcome = hasValue
                ? Outcomes.Success(lookupAircraft)
                : Outcomes.Failure<Aircraft>();

            return await Task.FromResult(outcome);
        }
    }
}