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
        public async Task<ActionResult<IEnumerable<Platform>>> GetAllPlatforms()
        {
            try
            {
                var platforms = await _context.Platforms.ToListAsync();
                return Ok(platforms);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatformById(int id)
        {
            try
            {
                var platform = await _context.Platforms.FindAsync(id);
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
        public async Task<ActionResult<Platform>> CreatePlatform(CreatePlatformDTO platformDTO)
        {
            try
            {
                var platformToAdd = new Platform
                {
                    Name = platformDTO.Name,
                    Description = platformDTO.Description,
                    PlatformType = platformDTO.PlatformType
                };

                _context.Platforms.Add(platformToAdd);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlatformById", new { id = platformToAdd.Id }, platformToAdd);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new platform record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Platform>> UpdatePlatform(int id, Platform platform)
        {
            try
            {
                if (id != platform.Id)
                    return BadRequest("Platform ID mismatch");

                var platformToUpdate = await _context.Platforms.FindAsync(id);

                if (platformToUpdate == null)
                    return NotFound($"Platform with ID {id} was not found");

                platformToUpdate.Name = platform.Name;
                platformToUpdate.Description = platform.Description;
                platformToUpdate.PlatformType = platform.PlatformType;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating platform record");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Platform>> DeletePlatform(int id)
        {
            try
            {
                var platformToDelete = await _context.Platforms.FindAsync(id);

                if (platformToDelete == null)
                    return NotFound($"Platform with ID {id} was not found");

                _context.Platforms.Remove(platformToDelete);

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
