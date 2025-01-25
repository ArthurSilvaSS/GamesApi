namespace GamesAPI.Models
{
    public class GamePlataform
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int PlataformId { get; set; }
        public Plataform Plataform { get; set; }
    }
}
