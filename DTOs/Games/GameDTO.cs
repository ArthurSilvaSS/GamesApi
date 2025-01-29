namespace GamesAPI.DTOs.Games
{
    public class GameDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public List<int> PlatformIds { get; set; }
    }
}