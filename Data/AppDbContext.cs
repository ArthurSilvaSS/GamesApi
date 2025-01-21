using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Game> Games { get; set; }
    }
}
