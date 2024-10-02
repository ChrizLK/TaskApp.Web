using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskApp.Web.Models;

namespace TaskApp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
