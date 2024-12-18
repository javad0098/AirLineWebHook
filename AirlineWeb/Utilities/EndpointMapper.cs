// Utilities/EndpointMapper.cs
using AirlineWeb.Features.FlightDetails;
using AirlineWeb.Features.WebhookSubscription;

namespace AirlineWeb.Utilities
{
    public static class EndpointMapper
    {
        public static void MapAllEndpoints(WebApplication app)
        {
            // Call all the feature-specific mapping methods here
            app.MapWebhookSubscriptionEndpoints();
            app.MapFlightsEndpoints();

            // Add more feature mappings as needed
        }
    }
}
