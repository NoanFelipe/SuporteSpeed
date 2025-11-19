using AutoMapper;
using SuporteSpeed.API.Data;
using SuporteSpeed.API.DTOs.SupportTicket;
using SuporteSpeed.API.DTOs.User;

namespace SuporteSpeed.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<SupportTicket, SupportTicketCreateDto>().ReverseMap();
            CreateMap<SupportTicket, SupportTicketUpdateDto>().ReverseMap();

            CreateMap<SupportTicket, SupportTicketReadOnlyDto>()
                .ForMember(q => q.Name, d => d.MapFrom(map => map.User.Name))
                .ReverseMap();
            CreateMap<SupportTicket, SupportTicketDetailsDto>()
                .ForMember(q => q.Name, d => d.MapFrom(map => map.User.Name))
                .ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();
        }
    }
}
