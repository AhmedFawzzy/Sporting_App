using Microsoft.EntityFrameworkCore;
using Sporting.Models;

namespace Sporting.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet< Team > Teams { get; set; }
        public DbSet< League > leagues { get; set; }
    }
}
