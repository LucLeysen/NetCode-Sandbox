using Microsoft.EntityFrameworkCore;

namespace WebApiCore
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
            
        }

        public DbSet<TicketItem> TicketItems { get; set; }

    }
}