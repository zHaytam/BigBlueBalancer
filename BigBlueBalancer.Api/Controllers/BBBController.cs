using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client.Helpers;
using BigBlueButton.Client.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Xml)]
    public abstract class BBBController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public BBBController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        protected Task<Server> GetAvailableServer() => _appDbContext
            .Servers
            .Where(s => s.Up)
            .OrderBy(s => s.Load)
            .FirstOrDefaultAsync();

        protected Task<List<Server>> GetAvailableServers() => _appDbContext.Servers.Where(s => s.Up).ToListAsync();

        protected Task<Meeting> GetMeeting(string meetingId) => _appDbContext
                .Meetings
                .Where(m => m.Running && m.MeetingID == meetingId)
                .Include(s => s.Server)
                .FirstOrDefaultAsync();

        protected bool IsChecksumValid<T>(string callName, T request, string checksum) where T : class
        {
            var query = ParametersExtractor.GenerateQueryString(request);
            var realChecksum = ChecksumGenerator.Generate(callName, _configuration["Secret"], query);
            return realChecksum == checksum;
        }
    }
}
