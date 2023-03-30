using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using ASP_Meeting_18.Models.ViewModels.UserViewModel;
using AutoMapper;

namespace ASP_Meeting_18.AutoMapperProfiles
{
    public class EditUserProfile : Profile
    {
        public EditUserProfile()
        {
            CreateMap<User, EditUserViewModel>().ReverseMap();
        }
    }
}
