namespace AirlineSendAgent.Clients
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using AirlineSendAgent.Dtos;
    using AirlineSendAgent.Models;
    using RabbitMQ.Client;

    public class WebhookClients : IWebhookClients
    {
        private readonly HttpClient _httpClient;

        public WebhookClients(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendWebhookNotification(FlightDetailChengePayLoadDto flightDetailChengePayLoadDto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(flightDetailChengePayLoadDto),
                Encoding.UTF8,
                "application/json");
            try
            {
                await _httpClient.PostAsync(flightDetailChengePayLoadDto.WebhookURI, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
