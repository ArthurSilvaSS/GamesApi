using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasData(
                new Game 
                { 
                    Id = 1,
                    Name = "Cyberpunk 2077",
                    Description = "Description Cyberpunk 2077 ",
                    imageUrl = "https://store-images.s-microsoft.com/image/apps.64394.13510798887568268.0b1b1b1b-0b1b-0b1b-0b1b-0b1b0b1b0b1b?w=672&h=378&q=80&mode=letterbox&background=%23FFE4E4E4&format=jpg",
                    Genre = "RPG",
                    Platform = "Steam",
                    Publisher = "CD Projekt Red"
                }
                
            );
        }
    }
}
