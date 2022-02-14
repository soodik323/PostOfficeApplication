using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PostOffice.WebApp.Contracts;
using PostOffice.WebApp.Services;

namespace PostOffice.WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient<IShipmentDataService, ShipmentDataService>(client =>
            {
            //client.BaseAddress = new Uri("https://postofficeestapi.azurewebsites.net");
            client.BaseAddress = new Uri("https://localhost:44388");
          
            });
            builder.Services.AddHttpContextAccessor();


            await builder.Build().RunAsync();
        }
    }
}
