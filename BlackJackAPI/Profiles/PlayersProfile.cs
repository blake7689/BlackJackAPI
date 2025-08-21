using AutoMapper;
using BlackJackAPI.Models;
using BlackJackAPI.Dtos;

namespace BlackJackAPI.Profiles
{
    public class PlayersProfile : Profile
    {
        public PlayersProfile()
        {
            CreateMap<Player, PlayerReadDto>();

            CreateMap<PlayerCreateDto, Player>();

            CreateMap<PlayerUpdateDto, Player>();

            CreateMap<PlayerDeleteDto, Player>();
        }
    }
}
