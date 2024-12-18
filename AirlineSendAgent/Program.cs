using AirlineSendAgent.App;
using AirlineSendAgent.Clients;
using AirlineSendAgent.Data;
using AirlineSendAgent.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirlineSendAgent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("AirlineConnection");

                services.AddSingleton<IAppHost, AppHost>();
                services.AddSingleton<SendAgentDBContext>(provider => DbContextFactory.CreateDbContext(provider));

                services.AddDbContext<SendAgentDBContext>(opt =>
                    opt.UseSqlServer(connectionString));
                services.AddScoped<IWebhookClients, WebhookClients>();
                services.AddHttpClient();
            }).Build();

            var appHost = host.Services.GetService<IAppHost>();
            if (appHost != null)
            {
                appHost.Run();
            }
            else
            {
                throw new InvalidOperationException("IAppHost service is not registered.");
            }
        }
    }
}
