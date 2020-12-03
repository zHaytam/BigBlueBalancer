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
        public BBBController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        protected AppDbContext AppDbContext { get; }

        protected Task<Server> GetAvailableServer() => AppDbContext
            .Servers
            .Where(s => s.Up)
            .OrderBy(s => s.Load)
            .FirstOrDefaultAsync();

        protected Task<List<Server>> GetAvailableServers() => AppDbContext.Servers.Where(s => s.Up).ToListAsync();

        protected Task<Meeting> GetMeeting(string meetingId) => AppDbContext
                .Meetings
                .Where(m => m.Running && m.MeetingID == meetingId)
                .Include(s => s.Server)
                .FirstOrDefaultAsync();
    }
}
