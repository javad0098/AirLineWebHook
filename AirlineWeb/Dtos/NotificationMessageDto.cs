namespace AirlineWeb.Dtos
{
    public class NotificationMessageDto
    {
        public NotificationMessageDto()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string WebhookType { get; set; }

        public string FlightCode { get; set; }

        public decimal OldPrice { get; set; }

        public decimal NewPrice { get; set; }
    }
}
