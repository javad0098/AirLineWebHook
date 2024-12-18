using System.Threading.Tasks;

using AirlineWeb.Common;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Features.WebhookSubscription
{
    public class WebhookSubscriptionService
    {
        private readonly AirlineDbContext context;
        private readonly IMapper mapper;

        public WebhookSubscriptionService(AirlineDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // Create a new webhook subscription with standardized ApiResponse<T>
        public async Task<ApiResponse<WebhookSubscriptionReadDto>> CreateSubscriptionAsync(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            try
            {
                // Check if a subscription with the same URI already exists
                var existingSubscription = await this.context.WebhookSubscriptions.FirstOrDefaultAsync(s => s.WebhookURI == webhookSubscriptionCreateDto.WebhookURI).ConfigureAwait(false);

                if (existingSubscription != null)
                {
                    // Return an error response if the subscription already exists
                    return ApiResponse<WebhookSubscriptionReadDto>.ErrorResponse("Webhook subscription with the same URI already exists.", ApiErrorType.AlreadyExists);
                }

                // Map the DTO to the model and create a new subscription
                var subscription = this.mapper.Map<Models.WebhookSubscription>(webhookSubscriptionCreateDto);
                subscription.Secret = Guid.NewGuid().ToString(); // Generate a secret
                subscription.WebhookPublisher = "WebHookPublisher"; // Generate a secret

                this.context.WebhookSubscriptions.Add(subscription);
                await this.context.SaveChangesAsync().ConfigureAwait(false);

                var subscriptionReadDto = this.mapper.Map<WebhookSubscriptionReadDto>(subscription);
                return ApiResponse<WebhookSubscriptionReadDto>.SuccessResponse(subscriptionReadDto); // Return success response
            }
            catch (Exception e)
            {
                return ApiResponse<WebhookSubscriptionReadDto>.ErrorResponse($"An error occurred while creating the subscription. {e.Message}", ApiErrorType.ServerError);
            }
        }

        // Get a subscription by its Secret with standardized ApiResponse<T>
        public async Task<ApiResponse<WebhookSubscriptionReadDto>> GetSubscriptionBySecretAsync(string secret)
        {
            try
            {
                if (string.IsNullOrEmpty(secret))
                {
                    return ApiResponse<WebhookSubscriptionReadDto>.ErrorResponse("Secret cannot be null or empty.", ApiErrorType.ValidationError);
                }

                var subscription = await this.context.WebhookSubscriptions.FirstOrDefaultAsync(s => s.Secret == secret).ConfigureAwait(false);

                if (subscription == null)
                {
                    return ApiResponse<WebhookSubscriptionReadDto>.ErrorResponse("Webhook subscription not found.", ApiErrorType.NotFound);
                }

                var subscriptionReadDto = this.mapper.Map<WebhookSubscriptionReadDto>(subscription);
                return ApiResponse<WebhookSubscriptionReadDto>.SuccessResponse(subscriptionReadDto); // Return success response
            }
            catch (Exception e)
            {
                return ApiResponse<WebhookSubscriptionReadDto>.ErrorResponse($"An error occurred while reading the subscription. {e.Message}", ApiErrorType.ServerError);
            }
        }
    }
}
