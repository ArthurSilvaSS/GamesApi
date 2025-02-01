using GamesAPI.DTOs.Games;
using Microsoft.AspNetCore.JsonPatch;

namespace GamesAPI.Services.Games
{
    public interface IGameService
    {
        Task<IEnumerable<GameDetailsDTO>> GetAllGames();
        Task<GameDetailsDTO> GetGameById(int id);
        Task<GameDetailsDTO> CreateGame(GameDTO gameDTO);
        Task<GameDetailsDTO> UpdateGame(int id, GameDTO gameDTO);
        Task<GameDetailsDTO> PartialUpdateGame(int id, JsonPatchDocument<GameUpdateDTO> patchDoc);
        Task<bool> DeleteGame(int id);

    }
}
