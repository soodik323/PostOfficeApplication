using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace PostOffice.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
