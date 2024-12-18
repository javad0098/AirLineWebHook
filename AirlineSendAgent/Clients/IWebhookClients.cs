using AirlineSendAgent.Clients;
using AirlineSendAgent.Dtos;

namespace AirlineSendAgent.Clients
{
    public interface IWebhookClients
    {
        Task SendWebhookNotification(FlightDetailChengePayLoadDto flightDetailChengePayLoadDto);
    }
}
