using Exercise_1.Application.Properties;
using Exercise_1.Domain.Flights;
using Exercise_1.Domain.SeedWork;
using Infrastructure.Data;
using Infrastructure.Gateway;
using Infrastructure.Properties;
using Microsoft.Extensions.DependencyInjection;

namespace Exercise_1.Api
{
    public static class DependencyInjectionInstaller
    {
        public static IServiceCollection InstallDependencies(this IServiceCollection services)
        {
            return services.AddDbContext()
                
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
                    cfg.RegisterServicesFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
                })
                .AddTransient<IFlightRepository, FlightRepositoryInMemory>()
                .AddTransient<IAircraftGateway, AircraftFakeGateway>()
                .AddTransient<IAirportGateway, AirportFakeGateway>();
        }
    }
}