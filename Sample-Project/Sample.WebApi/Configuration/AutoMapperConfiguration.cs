using AutoMapper;
using Sample.WebApi.DTO;
using Sample.WebApi.Models;

namespace Sample.WebApi.Configuration
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UserDto, UserPofile>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
