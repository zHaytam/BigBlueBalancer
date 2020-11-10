using BigBlueBalancer.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [ApiController]
    [Route("bigbluebutton/api")]
    [Produces(MediaTypeNames.Application.Xml)]
    public abstract class BBBController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BBBController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
    }
}
