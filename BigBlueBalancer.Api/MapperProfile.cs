using AutoMapper;
using BigBlueBalancer.Api.DTOs.Servers;
using BigBlueBalancer.Api.Entities;

namespace BigBlueBalancer.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Server, ServerDto>();
            CreateMap<NewServerDto, Server>();
        }
    }
}
