using TravelAgentWeb.Common;
using TravelAgentWeb.Dtos;

namespace TravelAgentWeb.Features.Notifications
{
    public static class NotificationsEndpoints
    {
        public static void MapFlightEndpoints(this IEndpointRouteBuilder app)
        {
            // POST: Handle webhook for flight changes
            app.MapPost("/api/Notification", async (FlightsService service, FlightDetailUpdateDto flightDetailUpdateDto) =>
            {
                var result = await service.FlightChenged(flightDetailUpdateDto).ConfigureAwait(false);

                // Handle the API response (either success or error)
                return ApiResponseHelper.HandleApiResponse(result);
            });
        }
    }
}
