using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using AutoMapper;

namespace ASP_Meeting_18.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
        }
    }
}
