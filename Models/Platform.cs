using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class Platform
    {
        public Platform()
        {
            GamePlatforms = new List<GamePlatform>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string PlatformType { get; set; }

        public ICollection<GamePlatform> GamePlatforms{ get; set; }
    }
}
