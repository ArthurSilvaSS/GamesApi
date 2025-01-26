namespace GamesAPI.DTOs
{
    public class GameDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public List<PlatformDTO> Platforms { get; set; }
    }
}
