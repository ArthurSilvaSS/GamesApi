namespace GamesAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string imageUrl { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }
    }
}