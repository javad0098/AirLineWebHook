using Microsoft.EntityFrameworkCore;

using TravelAgentWeb.Models;

namespace TravelAgentWeb.Data
{
    public class TavelAgentDbContext : DbContext
    {
        public TavelAgentDbContext(DbContextOptions<TavelAgentDbContext> opt)
            : base(opt)
        {
        }

        public DbSet<WebHookSecret> SubscriptiocnSecrets { get; set; }
    }
}
