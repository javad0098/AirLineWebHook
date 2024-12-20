using System.Text;
using System.Text.Json;
using AirlineWeb.Dtos;
using RabbitMQ.Client;

namespace AirlineWeb.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        public void SendMessage(NotificationMessageDto notificationMessageDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5673 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                var message = JsonSerializer.Serialize(notificationMessageDto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "trigger",
                    routingKey: string.Empty,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
