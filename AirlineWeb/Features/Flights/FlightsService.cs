using System.Threading.Tasks;

using AirlineWeb.Common;
using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.MessageBus;
using AirlineWeb.Models;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

namespace AirlineWeb.Features.WebhookSubscription
{
    public class FlightsService
    {
        private readonly AirlineDbContext context;
        private readonly IMapper mapper;
        private readonly IMessageBusClient messageBusClient;

        public FlightsService(AirlineDbContext context, IMapper mapper, IMessageBusClient messageBusClient)
        {
            this.context = context;
            this.mapper = mapper;
            this.messageBusClient = messageBusClient;
        }

        // Create a new webhook subscription with standardized ApiResponse<T>
        public async Task<ApiResponse<FlightDetailReadDto>> CreateFlightAsync(FlightDetailCreateDto flightDetailCreateDto)
        {
            try
            {
                // Check if a subscription with the same URI already exists
                var existingFlight = await this.context.FlightDetails.FirstOrDefaultAsync(s => s.FlightCode == flightDetailCreateDto.FlightCode).ConfigureAwait(false);

                if (existingFlight != null)
                {
                    // Return an error response if the subscription already exists
                    return ApiResponse<FlightDetailReadDto>.ErrorResponse("Webhook flight with the same code already exists.", ApiErrorType.AlreadyExists);
                }

                // Map the DTO to the model and create a new subscription
                var flight = this.mapper.Map<FlightDetail>(flightDetailCreateDto);

                this.context.FlightDetails.Add(flight);
                await this.context.SaveChangesAsync().ConfigureAwait(false);

                var flightDetailReadDto = this.mapper.Map<FlightDetailReadDto>(flight);
                return ApiResponse<FlightDetailReadDto>.SuccessResponse(flightDetailReadDto); // Return success response
            }
            catch (Exception e)
            {
                return ApiResponse<FlightDetailReadDto>.ErrorResponse($"An error occurred while creating the subscription. {e.Message} and the inner excseption is {e.InnerException}", ApiErrorType.ServerError);
            }
        }

        // Get a subscription by its Secret with standardized ApiResponse<T>
        public async Task<ApiResponse<FlightDetailReadDto>> GetFlightBytCodeAsync(string flightCode)
        {
            try
            {
                if (string.IsNullOrEmpty(flightCode))
                {
                    return ApiResponse<FlightDetailReadDto>.ErrorResponse("FlightCode cannot be null or empty.", ApiErrorType.ValidationError);
                }

                var flight = await this.context.FlightDetails.FirstOrDefaultAsync(s => s.FlightCode == flightCode).ConfigureAwait(false);

                if (flight == null)
                {
                    return ApiResponse<FlightDetailReadDto>.ErrorResponse("Flight not found.", ApiErrorType.NotFound);
                }

                var flightReadDto = this.mapper.Map<FlightDetailReadDto>(flight);
                return ApiResponse<FlightDetailReadDto>.SuccessResponse(flightReadDto); // Return success response
            }
            catch (Exception e)
            {
                return ApiResponse<FlightDetailReadDto>.ErrorResponse($"An error occurred while reading the flight. {e.Message}", ApiErrorType.ServerError);
            }
        }

        // Get a subscription by its Secret with standardized ApiResponse<T>
        public async Task<ApiResponse<FlightDetailUpdateDto>> UpdateFlightdetaile(int flightId, FlightDetailUpdateDto flightDetailUpdateDto)
        {
            try
            {
                var flight = await this.context.FlightDetails.FindAsync(flightId).ConfigureAwait(false);

                if (flight == null)
                {
                    return ApiResponse<FlightDetailUpdateDto>.ErrorResponse("Flight not found.", ApiErrorType.NotFound);
                }

                if (flightDetailUpdateDto == null)
                {
                    return ApiResponse<FlightDetailUpdateDto>.ErrorResponse("flight update deetailes can not be empty.", ApiErrorType.ValidationError);
                }

                decimal oldPrice = flight.Price;
                this.mapper.Map(flightDetailUpdateDto, flight);
                await this.context.SaveChangesAsync();
                if (oldPrice != flight.Price)
                {
                    var notificationMessageDto = new NotificationMessageDto
                    {
                        WebhookType = "PriceChange",
                        FlightCode = flight.FlightCode,
                        OldPrice = oldPrice,
                        NewPrice = flight.Price,
                    };
                    Console.WriteLine($"=>=>=>=>=> message is about to send: {notificationMessageDto}");

                    this.messageBusClient.SendMessage(notificationMessageDto);
                    Console.WriteLine($"=>=>=>=>=> message sent to message bus: {notificationMessageDto}");
                }

                Console.WriteLine($"Flight price updated - flightId: {flightId} FlightCode: {flight.FlightCode} - OldPrice: {oldPrice} - NewPrice: {flight.Price}");
                return ApiResponse<FlightDetailUpdateDto>.SuccessResponse(flightDetailUpdateDto); // Return success response
            }
            catch (Exception e)
            {
                return ApiResponse<FlightDetailUpdateDto>.ErrorResponse($"An error occurred while updating the flight. {e.Message}", ApiErrorType.ServerError);
            }
        }
    }
}
