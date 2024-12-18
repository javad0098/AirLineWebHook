using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineSendAgent.Data
{
    public static class DbContextFactory
    {
        public static SendAgentDBContext CreateDbContext(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<SendAgentDBContext>>();
            var context = new SendAgentDBContext(options);

            // Validate the connection string
            return context;
        }
    }
}
