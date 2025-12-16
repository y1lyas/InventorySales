using InventorySales.Application.Features.Users.Commands.Login;
using InventorySales.Application.Features.Users.Commands.Register;
using InventorySales.Application.Features.Users.DTOs;

namespace InventorySales.Application.Features.Users.Maps
{
    public class UserCommandMappingProfile : Profile
    {
        public UserCommandMappingProfile()
        {
            CreateMap<RegisterUserDto, RegisterUserCommand>();
            CreateMap<LoginUserDto, LoginUserCommand>();
        }
    }
}
