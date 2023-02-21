using AutoMapper;
using DemoBackend.Dtos;
using DemoBackend.Models;

namespace DemoBackend.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Cart, CartDto>().ReverseMap();
        }
    }
}
