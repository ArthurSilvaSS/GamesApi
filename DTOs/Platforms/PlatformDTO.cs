using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTOs.Platforms
{
    public class PlatformDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exced 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Platform type is required")]
        public string PlatformType { get; set; }
    }
}
