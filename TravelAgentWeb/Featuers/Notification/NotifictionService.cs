using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TravelAgentWeb.Common;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;
using TravelAgentWeb.Models;

namespace TravelAgentWeb.Features.Notifications
{
    public class FlightsService
    {
        private readonly TavelAgentDbContext context;
        private readonly IMapper mapper;

        public FlightsService(TavelAgentDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // Create a new webhook subscription with standardized ApiResponse<T>
        public async Task<ApiResponse<object>> FlightChenged(FlightDetailUpdateDto flightDetailUpdateDto)
        {
            try
            {
                // Check if a subscription with the same URI already exists
                var secreteModel = await this.context.SubscriptiocnSecrets.FirstOrDefaultAsync(s => s.Publisher == flightDetailUpdateDto.Publisher && s.Secret == flightDetailUpdateDto.Secret).ConfigureAwait(false);

                if (secreteModel == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalidate secrete, Ignore webhook");
                    Console.ResetColor();

                    // Return an error response if the subscription already exists
                    return ApiResponse<object>.ErrorResponse("Invalidate secrete, Ignore webhook", ApiErrorType.Unauthorized);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("valid webhook");
                    Console.WriteLine($"old price {flightDetailUpdateDto.OldPrice} , and the new price is {flightDetailUpdateDto.NewPrice}");
                    return ApiResponse<object>.SuccessResponse(null);
                }

                // Map the DTO to the model and create a new subscription
            }
            catch (Exception e)
            {
                return ApiResponse<object>.ErrorResponse($"An error occurred while creating the subscription. {e.Message} and the inner excseption is {e.InnerException}", ApiErrorType.ServerError);
            }
        }
    }
}
