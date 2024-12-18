using AirlineWeb.Dtos;
using AirlineWeb.Models;

using AutoMapper;

namespace AirlineWeb.Profiles
{
    public class WebhookSubscriptionProfile : Profile
    {
        public WebhookSubscriptionProfile()
        {
            this.CreateMap<WebhookSubscriptionCreateDto, WebhookSubscription>();
            this.CreateMap<WebhookSubscription, WebhookSubscriptionReadDto>();
        }
    }
}
