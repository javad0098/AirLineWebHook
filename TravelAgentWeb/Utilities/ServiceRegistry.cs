using TravelAgentWeb.Features.Notifications;

namespace TravelAgentWeb.Utilities
{
    public static class ServiceRegistry
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<FlightsService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
