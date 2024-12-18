// Utilities/EndpointMapper.cs
using TravelAgentWeb.Features.Notifications;

namespace TravelAgentWeb.Utilities
{
    public static class EndpointMapper
    {
        public static void MapAllEndpoints(WebApplication app)
        {
            app.MapFlightEndpoints();
        }
    }
}
