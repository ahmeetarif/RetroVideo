using AutoMapper;
using RetroVideo.Entities;

namespace RetroVideo.MappingProfiles
{
    public class MappingProfileG : Profile
    {
        public MappingProfileG()
        {
            CreateMap<Film, Film>();
            CreateMap<Genre, Genre>();
            CreateMap<Reservaty, Reservaty>();
            CreateMap<Klanten, Klanten>();
        }
    }
}