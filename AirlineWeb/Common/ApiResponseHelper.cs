using AirlineWeb.Common;

using Microsoft.AspNetCore.Http;

namespace AirlineWeb.Common
{
    public static class ApiResponseHelper
    {
        // Generic method to map ApiResponse<T> to appropriate IResult
        public static IResult HandleApiResponse<T>(ApiResponse<T> response)
        {
            if (response.Success)
            {
                return Results.Ok(response.Data); // Return 200 OK if the response was successful
            }

            // Handle different error types
            return response.ErrorType switch
            {
                ApiErrorType.ValidationError => Results.BadRequest(new { Error = response.ErrorMessage }),
                ApiErrorType.NotFound => Results.NotFound(new { Error = response.ErrorMessage }),
                ApiErrorType.Unauthorized => Results.Unauthorized(),
                ApiErrorType.Forbidden => Results.Json(new { Error = response.ErrorMessage }, statusCode: StatusCodes.Status403Forbidden),  // Use Results.Json for forbidden response
                ApiErrorType.ServerError => Results.Problem(response.ErrorMessage, statusCode: StatusCodes.Status500InternalServerError),
                _ => Results.BadRequest(new { Error = "An unknown error occurred." }) // Fallback for unknown errors
            };
        }
    }
}
