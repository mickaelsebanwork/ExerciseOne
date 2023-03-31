using Exercise_1.Domain.SeedWork;

namespace Exercise_1.Domain.Flights
{
    public sealed class Airport : Entity
    {
        private Airport()
        {

        }
        public Airport(string code, string name, string address, string city, string country, CoordinatePoints location)
        {
            Code = code;
            Name = name;
            Address = address;
            City = city;
            Country = country;
            Location = location;
        }

        public string Code { get; }
        public string Name { get; }
        public string Address { get; }
        public string City { get; }
        public string Country { get; }
        public CoordinatePoints Location { get; }
    }
}