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

            if (gameDTO.PlatformIds != null)
            {
                game.GamePlatforms = gameDTO.PlatformIds
                    .Select(platformId => new GamePlatform { PlatformId = platformId })
                    .ToList();
            }
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return _mapper.Map<GameDetailsDTO>(game);
        }

        public async Task<GameDetailsDTO?> UpdateGame(int id, GameDTO gameDTO)
        {
            var game = await _context.Games
                .Include(g => g.GamePlatforms)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
                return null;

            _mapper.Map(gameDTO, game);

            if (gameDTO.PlatformIds != null)
            {
                _context.GamePlatforms.RemoveRange(game.GamePlatforms);

                game.GamePlatforms = gameDTO.PlatformIds
                    .Select(platformId => new GamePlatform { PlatformId = platformId })
                    .ToList();
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<GameDetailsDTO>(game);
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
