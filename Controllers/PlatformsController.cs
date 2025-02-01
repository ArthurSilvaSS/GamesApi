using GamesAPI.Data;
using GamesAPI.DTOs.Platforms;
using GamesAPI.Models;
using GamesAPI.Services.Platform;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        public PlatformsController(IPlatformService platformService)
        {
            _platformService = platformService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformDTO>>> GetAllPlatforms()
        {
            return Ok(await _platformService.GetAllPlatforms());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformDTO>> GetPlatformById(int id)
        {
           var platform = await _platformService.GetPlatformById(id);
            return platform != null ? Ok(platform) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<Platform>> CreatePlatform(PlatformCreateDTO platformDTO)
        {
           var createdPlatform = await _platformService.CreatePlatform(platformDTO);
            return CreatedAtAction(nameof(GetPlatformById), new { id = createdPlatform.Id }, createdPlatform);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PlatformDTO>> UpdatePlatform(int id, PlatformDTO platformDTO)
        {
            var updatedPlatform = await _platformService.UpdatePlatform(id, platformDTO);
            return updatedPlatform != null ? Ok(updatedPlatform) : NotFound();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<PlatformDTO>> PartialUpdatePlatform(int id,[FromBody] JsonPatchDocument<PlatformDTO> patchDoc)
        {
            var updatedPlatform = await _platformService.PartialUpdate(id, patchDoc);
            return updatedPlatform != null ? Ok(updatedPlatform) : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Platform>> DeletePlatform(int id)
        {
            return await _platformService.DeletePlatform(id) ? NoContent() : NotFound();
        }
    }

}
