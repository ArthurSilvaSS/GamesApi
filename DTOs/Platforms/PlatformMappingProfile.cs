using AutoMapper;

namespace GamesAPI.DTOs.Platforms
{
    public class PlatformMappingProfile : Profile
    {
        public PlatformMappingProfile()
        {
            CreateMap<Models.Platform, PlatformDTO>();
            CreateMap<PlatformCreateDTO, Models.Platform>();
            CreateMap<PlatformDTO, Models.Platform>();
        }
    }
}
