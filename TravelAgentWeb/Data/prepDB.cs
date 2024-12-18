using System;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TravelAgentWeb.Models;

namespace TravelAgentWeb.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            // Create a scope to access services
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Get the required service - replace AppDbContext with your actual DbContext
                var context = serviceScope.ServiceProvider.GetService<TavelAgentDbContext>();

                // Check if context is not null
                if (context != null)
                {
                    SeedData(context);
                }
                else
                {
                    Console.WriteLine("Failed to get the AppDBContext.");
                }
            }
        }

        private static void SeedData(TavelAgentDbContext context)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;

            Console.WriteLine("init migration.");

            Console.ResetColor();

            context.Database.Migrate();
        }
    }
}
