using AutoMapper;
using GamesAPI.DTOs.Platforms;
using GamesAPI.Models;

namespace GamesAPI.DTOs.Games
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, GameDetailsDTO>();
            CreateMap<GameDTO, Game>();
            CreateMap<Platform, PlatformDTO>();
                
        }
    }
}
