using AutoMapper;
using GamesAPI.Data;
using GamesAPI.DTOs.Platforms;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services.Platform
{
    public class PlatformService : IPlatformService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PlatformService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PlatformDTO>> GetAllPlatforms()
        {
            return _mapper.Map<IEnumerable<PlatformDTO>>(await _context.Platforms.ToListAsync());
        }
        public async Task<PlatformDTO> GetPlatformById(int id)
        {
            return _mapper.Map<PlatformDTO>(await _context.Platforms.FindAsync(id));
        }
        public async Task<PlatformDTO> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            var platform = _mapper.Map<Models.Platform>(platformCreateDTO);
            _context.Platforms.Add(platform);
            await _context.SaveChangesAsync();

            return _mapper.Map<PlatformDTO>(platform);
        }
        public async Task<PlatformDTO> UpdatePlatform(int id, PlatformDTO platformUpdateDTO)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
                return null;

            _mapper.Map(platformUpdateDTO, platform);
            await _context.SaveChangesAsync();

            return _mapper.Map<PlatformDTO>(platform);
        }
        public async Task<PlatformDTO> PartialUpdate(int id, JsonPatchDocument<PlatformDTO> patchDoc)
        {
            var platform = await _context.Platforms.FindAsync(id);

            if (platform == null)
                return null;

            var platformDTO = _mapper.Map<PlatformDTO>(platform);
            patchDoc.ApplyTo(platformDTO);

            _mapper.Map(platformDTO, platform);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlatformDTO>(platform);
        }
        public async Task<bool> DeletePlatform(int id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
                return false;

            _context.Platforms.Remove(platform);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
