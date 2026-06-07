using InventorySales.Application.Features.Categories.DTOs;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Features.Categories.Maps
{
    public class CategoryQueryMappingProfile : Profile
    {
        public CategoryQueryMappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
