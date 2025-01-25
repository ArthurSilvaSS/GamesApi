using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Models
{
    public class Plataform
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string PlataformType { get; set; }

        public ICollection<GamePlataform> GamePlataforms{ get; set; }
    }
}
