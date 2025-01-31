using AutoMapper;
using GamesAPI.Data;
using GamesAPI.DTOs.Games;
using GamesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public GameService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDetailsDTO?>> GetAllGames()
        {
            var games = await _context.Games
                .Include(g => g.GamePlatforms)
                .ThenInclude(gp => gp.Platform)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GameDetailsDTO>>(games);
        }

        public async Task<GameDetailsDTO> GetGameById(int id)
        {
            var game = await _context.Games
                .Include(g => g.GamePlatforms)
                .ThenInclude(gp => gp.Platform)
                .FirstOrDefaultAsync(g => g.Id == id);

            return _mapper.Map<GameDetailsDTO>(game);
        }

        public async Task<GameDetailsDTO> CreateGame(GameDTO gameDTO)
        {
            var game = _mapper.Map<Game>(gameDTO);

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            if (gameDTO.PlatformIds != null && gameDTO.PlatformIds.Any())
            {
                var platformsExist = await _context.Platforms
                    .Where(p => gameDTO.PlatformIds.Contains(p.Id))
                    .CountAsync();

                if (platformsExist != gameDTO.PlatformIds.Count)
                    throw new Exception("Uma ou mais plataformas não existem");

                game.GamePlatforms = gameDTO.PlatformIds
                    .Select(platformId => new GamePlatform
                    {
                        GameId = game.Id,
                        PlatformId = platformId
                    })
                    .ToList();

                await _context.SaveChangesAsync();
            }

            await _context.Entry(game)
                .Collection(g => g.GamePlatforms)
                .Query()
                .Include(gp => gp.Platform)
                .LoadAsync();

            return _mapper.Map<GameDetailsDTO>(game);
        }

        public async Task<GameDetailsDTO> UpdateGame(int id, GameDTO gameDTO)
        {
          
            var gameToUpdate = await _context.Games
                .Include(g => g.GamePlatforms)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToUpdate == null)
                return null;

            if (gameDTO.PlatformIds != null && gameDTO.PlatformIds.Any())
            {
                var existingPlatforms = await _context.Platforms
                    .Where(p => gameDTO.PlatformIds.Contains(p.Id))
                    .ToListAsync();

                if (existingPlatforms.Count != gameDTO.PlatformIds.Count)
                    throw new Exception("One or more platforms do not exist");
            }

            _mapper.Map(gameDTO, gameToUpdate);

            _context.GamePlatforms.RemoveRange(gameToUpdate.GamePlatforms);

            if (gameDTO.PlatformIds != null && gameDTO.PlatformIds.Any())
            {
                gameToUpdate.GamePlatforms = gameDTO.PlatformIds
                    .Select(platformId => new GamePlatform
                    {
                        GameId = gameToUpdate.Id,
                        PlatformId = platformId
                    })
                    .ToList();
            }

            await _context.SaveChangesAsync();

            await _context.Entry(gameToUpdate)
                .Collection(g => g.GamePlatforms)
                .Query()
                .Include(gp => gp.Platform)
                .LoadAsync();

            return _mapper.Map<GameDetailsDTO>(gameToUpdate);
        }
        public async Task<bool> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
