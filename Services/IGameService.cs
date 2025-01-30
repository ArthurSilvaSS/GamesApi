using GamesAPI.DTOs.Games;

namespace GamesAPI.Services
{
    public interface IGameService
    {
        Task<IEnumerable<GameDetailsDTO>> GetAllGames();
        Task<GameDetailsDTO> GetGameById(int id);
        Task<GameDetailsDTO> CreateGame(GameDTO gameDTO);
        Task<GameDetailsDTO> UpdateGame(int id, GameDTO gameDTO);
        Task<bool> DeleteGame(int id);

    }
}
