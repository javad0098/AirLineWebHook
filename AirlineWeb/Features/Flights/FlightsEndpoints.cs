using System.Threading.Tasks;

using AirlineWeb.Common;
using AirlineWeb.Dtos;
using AirlineWeb.Features.WebhookSubscription;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AirlineWeb.Features.FlightDetails
{
    public static class FlightsEndpoints
    {
        public static void MapFlightsEndpoints(this WebApplication app)
        {
            // POST: Create a new flight
            app.MapPost("/api/flights", async (FlightsService service, FlightDetailCreateDto dto) =>
            {
                var response = await service.CreateFlightAsync(dto).ConfigureAwait(false);

                if (!response.Success)
                {
                    // Handle errors using the ApiResponseHelper
                    return ApiResponseHelper.HandleApiResponse(response);
                }

                // Generate the location URI for the created flight (using the flight code)
                var locationUri = $"/api/flights/{response.Data!.FlightCode}";

                // Return 201 Created with the location of the created flight
                return Results.Created(locationUri, response.Data);
            });

            // GET: Get a flight by flight code
            app.MapGet("/api/flights/{flightCode}", async (FlightsService service, string flightCode) =>
            {
                var response = await service.GetFlightBytCodeAsync(flightCode).ConfigureAwait(false);

                // Use the helper method to handle the response
                return ApiResponseHelper.HandleApiResponse(response);
            });

            // PUT: Update flight details by ID
            app.MapPut("/api/flights/{id:int}", async (FlightsService service, int id, FlightDetailUpdateDto dto) =>
            {
                var response = await service.UpdateFlightdetaile(id, dto).ConfigureAwait(false);
                return ApiResponseHelper.HandleApiResponse(response);
            });
        }
    }
}
