using GamesAPI.DTOs.Platforms;
using Microsoft.AspNetCore.JsonPatch;

namespace GamesAPI.Services.Platform
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDTO>> GetAllPlatforms();
        Task<PlatformDTO> GetPlatformById(int id);
        Task<PlatformDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO);
        Task<PlatformDTO> UpdatePlatform(int id, PlatformDTO platformUpdateDTO);
        Task<PlatformDTO> PartialUpdate(int id, JsonPatchDocument<PlatformDTO> patchDoc);
        Task<bool> DeletePlatform(int id);
    }
}
