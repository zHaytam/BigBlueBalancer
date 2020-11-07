using AutoMapper;
using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [Route("bigbluebutton/api")]
    public class MonitoringController : BBBController
    {
        private readonly IBBBClient _bbbClient;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public MonitoringController(IBBBClient bbbClient, AppDbContext appDbContext, IMapper mapper,
            IConfiguration configuration) : base(appDbContext, configuration)
        {
            _bbbClient = bbbClient;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("isMeetingRunning")]
        public async Task<IsMeetingRunningResponse> IsMeetingRunning(string meetingID)
        {
            var meeting = await _appDbContext
                .Meetings
                .Where(m => m.Running && m.MeetingID == meetingID)
                .Include(s => s.Server)
                .FirstOrDefaultAsync();

            if (meeting == null)
            {
                return new IsMeetingRunningResponse
                {
                    ReturnCode = "SUCCESS",
                    Running = "false"
                };
            }

            return await _bbbClient.IsMeetingRunning(meeting.Server.Url, meeting.Server.Secret, meetingID);
        } 
    }
}
