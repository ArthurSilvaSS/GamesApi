using AutoMapper;
using GamesAPI.DTOs.Platforms;
using GamesAPI.Models;

namespace GamesAPI.DTOs.Games
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GameDetailsDTO>()
                .ForMember(dest => dest.Platforms,
                           opt => opt.MapFrom(src => src.GamePlatforms.Select(gp => gp.Platform)));
            CreateMap<Game, GameUpdateDTO>();
            CreateMap<GameUpdateDTO, Game>();
            CreateMap<Platform, PlatformDTO>();
                
        }
    }
}
