using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Game> Games { get; set; }
        public DbSet<Plataform> Plataforms { get; set; }
        public DbSet<GamePlataform> GamePlataforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GamePlataform>()
                .HasKey(gp => new { gp.GameId, gp.PlataformId });

            modelBuilder.Entity<GamePlataform>()
                .HasOne(gp => gp.Game)
                .WithMany(g => g.GamePlataforms)
                .HasForeignKey(gp => gp.GameId);

            modelBuilder.Entity<GamePlataform>()
                .HasOne(gp => gp.Plataform)
                .WithMany(p => p.GamePlataforms)
                .HasForeignKey(gp => gp.PlataformId);
        }
    }
}
