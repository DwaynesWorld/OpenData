using AutoMapper;
using FoodStore.Dtos;
using FoodStore.Entities;

namespace FoodStore.MappingProfiles
{
    public class FoodMappings : Profile
    {
        public FoodMappings()
        {
            CreateMap<FoodItem, FoodItemDto>().ReverseMap();
            CreateMap<FoodItem, FoodUpdateDto>().ReverseMap();
            CreateMap<FoodItem, FoodCreateDto>().ReverseMap();
        }
    }
}
