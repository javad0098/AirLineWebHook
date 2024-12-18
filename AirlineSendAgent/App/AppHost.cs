using System;
using System.Data.Common;

using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirlineSendAgent.Clients;
using AirlineSendAgent.Data;
using AirlineSendAgent.Dtos;
using AirlineSendAgent.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AirlineSendAgent.App
{
    public class AppHost : IAppHost
    {
        private readonly SendAgentDBContext _context;
        private readonly IWebhookClients _webhookClients;

        public AppHost(SendAgentDBContext context, IWebhookClients webhookClients)
        {
            _context = context;
            _webhookClients = webhookClients;
        }

        public void Run()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5673 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(
                queue: queueName,
                exchange: "trigger",
                routingKey: string.Empty);

                var consumer = new EventingBasicConsumer(channel);
                Console.WriteLine("Waiting for messages...");
                consumer.Received += async (moduleHandle, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var notificationMessageDto = JsonSerializer.Deserialize<NotificationMessageDto>(message);
                    System.Console.WriteLine($"[Received] {message}");

                    var webhookToSend = new FlightDetailChengePayLoadDto
                    {
                        WebhookType = notificationMessageDto.WebhookType,

                        Secret = string.Empty,
                        Publisher = string.Empty,
                        OldPrice = notificationMessageDto.OldPrice,
                        NewPrice = notificationMessageDto.NewPrice,
                        FlightCode = notificationMessageDto.FlightCode,
                    };
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Sending Webhook notification to {notificationMessageDto.WebhookType} subscribers...");

                    var i = _context.WebhookSubscriptions.Where(w => w.WebhookType == notificationMessageDto.WebhookType).Count();
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine("Number of webhooks: " + i.ToString() + " and type is" + notificationMessageDto.WebhookType);
                    foreach (var webhook in _context.WebhookSubscriptions.Where(w => w.WebhookType == notificationMessageDto.WebhookType))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"Sending Webhook notification to  subscribers...");

                        Console.WriteLine("Webhook to select " + webhook.WebhookPublisher);
                        webhookToSend.Secret = webhook.Secret;
                        webhookToSend.WebhookURI = webhook.WebhookURI;
                        webhookToSend.Publisher = webhook.WebhookPublisher;
                        Console.WriteLine($"Webhook to send: {webhook.WebhookURI} , secret: {webhook.Secret}");
                        await _webhookClients.SendWebhookNotification(webhookToSend);
                    }
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                System.Console.WriteLine("Listening for messages...");
                System.Console.ReadLine();
            }
        }
    }
}
