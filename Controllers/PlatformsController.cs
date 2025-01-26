using GamesAPI.Data;
using GamesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PlatformsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plataform>>> GetAllPlatforms()
        {
            try
            {
                var platforms = await _context.Plataforms.ToListAsync();
                return Ok(platforms);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plataform>> GetPlatformById(int id)
        {
            try
            {
                var platform = await _context.Plataforms.FindAsync(id);
                if (platform == null)
                    return NotFound($"Platform with ID {id} was not found");
                return Ok(platform);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plataform>> CreatePlatform(Plataform platform)
        {
            try
            {
                if (platform == null)
                    return BadRequest("Platform data is required");

                _context.Plataforms.Add(platform);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new platform record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Plataform>> UpdatePlatform(int id, Plataform platform)
        {
            try
            {
                if (id != platform.Id)
                    return BadRequest("Platform ID mismatch");

                var platformToUpdate = await _context.Plataforms.FindAsync(id);

                if (platformToUpdate == null)
                    return NotFound($"Platform with ID {id} was not found");

                platformToUpdate.Name = platform.Name;
                platformToUpdate.Description = platform.Description;
                platformToUpdate.PlataformType = platform.PlataformType;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating platform record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Plataform>> DeletePlatform(int id)
        {
            try
            {
                var platformToDelete = await _context.Plataforms.FindAsync(id);

                if (platformToDelete == null)
                    return NotFound($"Platform with ID {id} was not found");

                _context.Plataforms.Remove(platformToDelete);

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting platform record");
            }
        }
    }

}
