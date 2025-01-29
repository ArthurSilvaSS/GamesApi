using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs.Platforms
{
    public class PlatformCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string PlatformType { get; set; }
    }
}
