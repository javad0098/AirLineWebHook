// Utilities/ServiceRegistry.cs
using AirlineWeb.Features.WebhookSubscription;
using AirlineWeb.MessageBus;

namespace AirlineWeb.Utilities
{
    public static class ServiceRegistry
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<WebhookSubscriptionService>();
            services.AddScoped<FlightsService>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
