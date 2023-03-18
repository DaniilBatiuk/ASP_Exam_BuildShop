using ASP_Meeting_18.Data;
using ASP_Meeting_18.Models.DTO;
using AutoMapper;

namespace ASP_Meeting_18.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
