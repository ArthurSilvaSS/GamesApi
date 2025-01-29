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
                var platforms = await _context.Platforms.ToListAsync();
                var platformDTOs = platforms.Select(p => MapToPlatformDTO(p)).ToList();

                return Ok(platforms);
            }
            catch (Exception)
            {
                return HandleException();
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDTO>> GetPlatformById(int id)
        {
            try
            {
                var platform = await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);

                if (platform == null)
                    return NotFound($"Platform with ID {id} was not found");

                var platformDTO = MapToPlatformDTO(platform);
                return Ok(platform);
            }
            catch (Exception)
            {
                return HandleException();
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

               var responseDTO = MapToPlatformDetailsDTO(platformToAdd);
                return CreatedAtAction("GetPlatformById", new { id = responseDTO.Id }, responseDTO);
            }
            catch (Exception)
            {
                return HandleException();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PlatformDTO>> UpdatePlatform(int id, PlatformDTO platformDTO)
        {
            try
            {
                var platformToUpdate = await GetPlatformByIdAsync(id);
                if (platformToUpdate == null)
                    return NotFound($"Platform with ID {id} was not found");
                
                platformToUpdate.Name = platformDTO.Name;
                platformToUpdate.Description = platformDTO.Description;
                platformToUpdate.PlatformType = platformDTO.PlatformType;

                await _context.SaveChangesAsync();

                return Ok(platformDTO);
            }
            catch (Exception)
            {
                return HandleException();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Platform>> DeletePlatform(int id)
        {
            try
            {
                var platformToDelete = await GetPlatformByIdAsync(id);

                if (platformToDelete == null)
                    return NotFound($"Platform with ID {id} was not found");

                _context.Platforms.Remove(platformToDelete);

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return HandleException();
            }
        }
        private PlatformDTO MapToPlatformDTO(Platform platform)
        {
            return new PlatformDTO
            {
                Id = platform.Id,
                Name = platform.Name,
                Description = platform.Description,
                PlatformType = platform.PlatformType
            };
        }

        private static PlatformDetailsDTO MapToPlatformDetailsDTO(Platform? platformToUpdate)
        {
            return new PlatformDetailsDTO
            {
                Id = platformToUpdate.Id,
                Name = platformToUpdate.Name,
                Description = platformToUpdate.Description,
                PlatformType = platformToUpdate.PlatformType
            };
        }

        private async Task<Platform?> GetPlatformByIdAsync(int id)
        {
            return await _context.Platforms.FindAsync(id);
        }

        private ActionResult HandleException()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting platform record");
        }
    }

}
