using AutoMapper;
using SuporteSpeed.API.Data;
using SuporteSpeed.API.DTOs.User;

namespace SuporteSpeed.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();
            CreateMap<UserReadOnlyDto, User>().ReverseMap();
        }
    }
}
