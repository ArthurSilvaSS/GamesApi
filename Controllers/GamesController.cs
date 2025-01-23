using GamesAPI.Data;
using GamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]

    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            try
            {
                var games = await _context.Games.ToListAsync();
                return Ok(games);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            try { 
                var game = await _context.Games.FindAsync(id);

                if (game == null)
                    return NotFound($"Game with ID {id} was not found");
                
                return Ok(game);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame([FromBody] Game game)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest($"Invalid data ... check and try again");

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new game record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> UpdateGame(int id, [FromBody] Game updateGame)
        {
            try
            {
                if (id != updateGame.Id)
                    return BadRequest("The Id provided in the URL does not match the Id in the request body");

                var gameToUpdate = await _context.Games.FindAsync(id);

                if (gameToUpdate == null)
                    return NotFound($"Game with ID {id} was not found");

                gameToUpdate.Name = updateGame.Name;
                gameToUpdate.Description = updateGame.Description;
                gameToUpdate.imageUrl = updateGame.imageUrl;
                gameToUpdate.Genre = updateGame.Genre;
                gameToUpdate.Platform = updateGame.Platform;
                gameToUpdate.Publisher = updateGame.Publisher;

                await _context.SaveChangesAsync();

                return Ok(gameToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating game record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);

                if (game == null)
                    return NotFound($"Game with ID {id} was not found");

                _context.Games.Remove(game);
                await _context.SaveChangesAsync();

                return Ok(game);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting game record");
            }
        }
    }
}
