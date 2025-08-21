using BlackJackAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackJackAPI.DbContext
{
    public class BlackJackAPIDbContext : IdentityDbContext
    {
        public BlackJackAPIDbContext(DbContextOptions<BlackJackAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
    }
}
