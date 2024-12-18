using System;
using AirlineSendAgent.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirlineSendAgent.Data
{
    public class SendAgentDBContext : DbContext
    {
        public SendAgentDBContext(DbContextOptions<SendAgentDBContext> opt)
            : base(opt)
        {
        }

        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
    }
}
