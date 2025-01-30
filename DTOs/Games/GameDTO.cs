using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs.Games
{
    public class GameDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name is too long")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description is too long")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }


        public List<int> PlatformIds { get; set; }
    }
}