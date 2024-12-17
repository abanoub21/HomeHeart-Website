using AutoMapper;
using Home_Heart.Application.Dtos;
using Home_Heart.Domain;

namespace Home_Heart.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();  
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Catalogue, opt => opt.MapFrom(src => Convert.ToBase64String(src.Catalogue))).ReverseMap();

        }
    }
}//application token
