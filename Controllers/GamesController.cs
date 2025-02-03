using AutoMapper;
using GamesAPI.Data;
using GamesAPI.DTOs.Games;
using GamesAPI.Models;
using GamesAPI.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]

    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
       
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDetailsDTO>>> GetAllGames()
        {
            var games = await _gameService.GetAllGames();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDetailsDTO>> GetGameById(int id)
        {
            var game =await _gameService.GetGameById(id);
            if (game == null)
               throw new KeyNotFoundException($"Game with ID {id} not found");

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame([FromBody] GameDTO gameDTO)
        {
           var CreatedGame = await _gameService.CreateGame(gameDTO);

           return CreatedAtAction(nameof(GetGameById), new {id = CreatedGame.Id}, CreatedGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameDetailsDTO>> UpdateGame(int id, [FromBody] GameDTO gameDTO)
        {
           var gameToUpdate = await _gameService.UpdateGame(id, gameDTO);

            if (gameToUpdate == null)
                return NotFound($"Game with ID {id} not found");

            return Ok(gameToUpdate);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<GameDetailsDTO>> PartialUpdateGame(int id, [FromBody] JsonPatchDocument<GameUpdateDTO> patchDoc)
        {
            var updatedGame = await _gameService.PartialUpdateGame(id, patchDoc);
            return updatedGame != null ? Ok(updatedGame) : NotFound($"Game with ID {id} not found");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var result = await _gameService.DeleteGame(id);

            if (!result)
                return NotFound($"Game with ID {id} not found");

            return NoContent();
        }
    }
}
