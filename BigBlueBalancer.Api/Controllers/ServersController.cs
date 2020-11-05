using AutoMapper;
using AutoMapper.QueryableExtensions;
using BigBlueBalancer.Api.DTOs.Servers;
using BigBlueBalancer.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [ApiController]
    [Route("servers")]
    public class ServersController
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ServersController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ServerDto>> List()
            => await _appDbContext.Servers.ProjectTo<ServerDto>(_mapper.ConfigurationProvider).ToListAsync();

        [HttpPost]
        public async Task<ServerDto> Create(NewServerDto dto)
        {
            var server = await _appDbContext.Servers.AddAsync(_mapper.Map<Server>(dto));
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<ServerDto>(server.Entity);
        }
    }
}
