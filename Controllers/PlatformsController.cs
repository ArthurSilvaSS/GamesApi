using GamesAPI.Data;
using GamesAPI.DTOs;
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
        public async Task<ActionResult<IEnumerable<PlatformDTO>>> GetAllPlatforms()
        {
            try
            {
                var platforms = await _context.Platforms
                    .Include(p => p.GamePlatforms)
                    .Select(p => new PlatformDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        PlatformType = p.PlatformType
                    }).ToListAsync();

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
        public async Task<ActionResult<PlatformDetailsDTO>> UpdatePlatform(int id, PlatformDTO platformDTO)
        {
            try
            {
                var platformToUpdate = await _context.Platforms.FindAsync(id);

                if (platformToUpdate == null)
                {
                    return NotFound($"Platform with ID {id} was not found");
                }

                platformToUpdate.Name = platformDTO.Name;
                platformToUpdate.Description = platformDTO.Description;
                platformToUpdate.PlatformType = platformDTO.PlatformType;

                await _context.SaveChangesAsync();

                var responseDTO = new PlatformDetailsDTO
                {
                    Id = platformToUpdate.Id,
                    Name = platformToUpdate.Name,
                    Description = platformToUpdate.Description,
                    PlatformType = platformToUpdate.PlatformType
                };

                return Ok(responseDTO);
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
