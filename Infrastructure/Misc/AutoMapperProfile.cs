using AutoMapper;
using Domain.Entities;
using Infrastructure.Identity;

namespace Infrastructure.Misc
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, AppUser>().ReverseMap();
        }
    }
}
