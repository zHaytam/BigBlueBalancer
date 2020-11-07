using AutoMapper;
using BigBlueBalancer.Api.DTOs.Administration;
using BigBlueBalancer.Api.DTOs.Servers;
using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;

namespace BigBlueBalancer.Api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Server, ServerDto>();
            CreateMap<CreateOrEditServerDto, Server>();
            CreateMap<CreateMeetingDto, CreateRequest>();
            CreateMap<CreateResponse, Entities.Meeting>();
            CreateMap<Entities.Meeting, CreateResponse>();
        }
    }
}
