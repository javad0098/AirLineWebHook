using System;
using System.Linq;

using AirlineWeb.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirlineWeb.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            // Create a scope to access services
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Get the required service - replace AppDbContext with your actual DbContext
                var context = serviceScope.ServiceProvider.GetService<AirlineDbContext>();

                // Check if context is not null
                if (context != null)
                {
                    Console.WriteLine("===>>> call migration 0.");

                    SeedData(context);
                }
                else
                {
                    Console.WriteLine("Failed to get the AppDBContext.");
                }
            }
        }

        private static void SeedData(AirlineDbContext context)
        {
            context.Database.Migrate();

            // if (isProd)
            // {

            // try
            //     {
            //         Console.WriteLine("--> attemping to apply migrations ...");
            //         context.Database.Migrate();
            //     }
            //     catch (Exception ex)
            //     {
            //         Console.WriteLine($"--> could mot run migration: {ex.Message}");
            //     }
            // }
            // Check if there is already data in the table
            // if (!context.Certificates.Any())
            // {

            // Console.WriteLine("Seeding data...");

            // // Add some sample data
            //     context.Certificates.AddRange(
            //     new Certificate { Name = "AWS Certified Solutions Architect - Associate", Code = "AWS-SAA", Description = "Design and deploy scalable, highly available systems on AWS.", Cost = "$150", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "AWS Certified Developer - Associate", Code = "AWS-CDA", Description = "Develop and maintain applications on AWS.", Cost = "$150", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "AWS Certified SysOps Administrator - Associate", Code = "AWS-SOA", Description = "Manage and operate systems on AWS.", Cost = "$150", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Microsoft Certified: Azure Fundamentals", Code = "AZ-900", Description = "Basic understanding of cloud services and how Microsoft Azure provides them.", Cost = "$99", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Microsoft Certified: Azure Administrator Associate", Code = "AZ-104", Description = "Manage Azure subscriptions, implement storage, and manage virtual networks.", Cost = "$165", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Microsoft Certified: Azure Solutions Architect Expert", Code = "AZ-305", Description = "Design and implement solutions that run on Microsoft Azure.", Cost = "$165", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Google Associate Cloud Engineer", Code = "ACE", Description = "Deploy applications, monitor operations, and manage enterprise solutions on Google Cloud.", Cost = "$125", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Google Professional Cloud Architect", Code = "PCA", Description = "Design, develop, and manage robust, secure, scalable, and dynamic solutions to drive business objectives.", Cost = "$200", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Google Professional Data Engineer", Code = "PDE", Description = "Enable data-driven decision making by collecting, transforming, and publishing data.", Cost = "$200", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "AWS Certified Cloud Practitioner", Code = "AWS-CCP", Description = "Understanding of AWS Cloud concepts, billing, pricing models, and security.", Cost = "$100", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Microsoft Certified: Azure Security Engineer Associate", Code = "AZ-500", Description = "Manage identity and access, implement platform protection, and secure data.", Cost = "$165", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "AWS Certified DevOps Engineer - Professional", Code = "AWS-DevOps", Description = "Implement and manage continuous delivery systems and methodologies on AWS.", Cost = "$300", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Google Professional Cloud Developer", Code = "PCD", Description = "Design, build, and deploy applications that integrate Google Cloud services.", Cost = "$200", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "Microsoft Certified: Azure AI Engineer Associate", Code = "AI-102", Description = "Use cognitive services, machine learning, and knowledge mining to architect and implement Microsoft AI solutions.", Cost = "$165", CreatedDate = DateTime.Now },
            //     new Certificate { Name = "AWS Certified Machine Learning - Specialty", Code = "AWS-MLS", Description = "Design and implement cost-optimized, scalable, reliable, and secure ML solutions.", Cost = "$300", CreatedDate = DateTime.Now }
            // );

            // // Save changes to the database
            //     context.SaveChanges();
            // }
            // else
            // {
            //     Console.WriteLine("Data already exists, skipping seeding.");
            // }
        }
    }
}
