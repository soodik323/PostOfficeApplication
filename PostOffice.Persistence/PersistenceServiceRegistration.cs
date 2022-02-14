using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Persistence.Helpers;
using PostOffice.Persistence.Repositories;

namespace PostOffice.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<PostOfficeDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
               
            });

            services.AddScoped<IDataContext, PostOfficeDbContext>();
            services.AddSingleton<IRepositoryFactory, EfRepositoryFactory>();
            services.AddScoped<IRepositoryProvider, EfRepositoryProvider>();
            services.AddScoped<IPostOfficeEfUow, PostOfficeEfUow>();

            return services;
        }
    }
}
