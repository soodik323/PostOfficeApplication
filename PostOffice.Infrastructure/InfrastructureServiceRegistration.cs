using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Contracts.Infrastructure;

namespace PostOffice.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddScoped<IShipmentsDataService, ShipmentsDataService>();
            

            return services;
        }
    }
}
