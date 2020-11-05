using AutoMapper;
using BigBlueBalancer.Api.DTOs;
using BigBlueBalancer.Api.Entities;

namespace BigBlueBalancer.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Server, ServerDto>();
        }
    }
}
