using AirlineWeb.Common;
using AirlineWeb.Dtos;
using AirlineWeb.Features.WebhookSubscription;

using Microsoft.AspNetCore.Builder;

namespace AirlineWeb.Features.WebhookSubscription
{
    public static class WebhookSubscriptionEndpoints
    {
        public static void MapWebhookSubscriptionEndpoints(this WebApplication app)
        {
            // POST: Create a new webhook subscription
            app.MapPost("/api/webhooksubscriptions", async (WebhookSubscriptionService service, WebhookSubscriptionCreateDto dto) =>
            {
                var response = await service.CreateSubscriptionAsync(dto).ConfigureAwait(false);
                if (!response.Success)
                {
                    // Use the helper method to handle errors
                    return ApiResponseHelper.HandleApiResponse(response);
                }

                // Generate the location URI for the created subscription (using the secret)
                var locationUri = $"/api/webhooksubscriptions/{response.Data!.Secret}";

                // Return 201 Created with the location of the created subscription
                return Results.Created(locationUri, response.Data);
            });

            // GET: Get a webhook subscription by Secret
            app.MapGet("/api/webhooksubscriptions/{secret}", async (WebhookSubscriptionService service, string secret) =>
            {
                var response = await service.GetSubscriptionBySecretAsync(secret).ConfigureAwait(false);

                // Use the helper method to handle the response
                return ApiResponseHelper.HandleApiResponse(response);
            });
        }
    }
}
