using AutoMapper;
using BigBlueBalancer.Api.DTOs;
using BigBlueBalancer.Api.DTOs.Administration;
using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    public class AdministrationController : BBBController
    {
        private readonly IBBBClient _bbbClient;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AdministrationController(IBBBClient bbbClient, AppDbContext appDbContext, IMapper mapper) : base(appDbContext)
        {
            _bbbClient = bbbClient;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("create")]
        [ProducesResponseType(typeof(CreateResponse), 200)]
        public async Task<IActionResult> Create([FromQuery] CreateMeetingDto dto)
        {
            var existingMeeting = await _appDbContext.Meetings.FirstOrDefaultAsync(m => m.Running && m.MeetingID == dto.MeetingId);
            if (existingMeeting != null)
            {
                var earlyResponse = _mapper.Map<CreateResponse>(existingMeeting);
                earlyResponse.ReturnCode = "SUCCESS";
                earlyResponse.MessageKey = "duplicateWarning";
                earlyResponse.Message = "This conference was already in existence and may currently be in progress.";
                return Ok(earlyResponse);
            }

            var server = await GetAvailableServer();
            if (server == null)
            {
                return Ok(new BaseBBBResponse
                {
                    ReturnCode = "FAILED",
                    MessageKey = "unavailableServer",
                    Message = "[BigBlueBalancer] Unavailble server"
                });
            }

            var request = _mapper.Map<CreateRequest>(dto);
            var response = await _bbbClient.Create(server.Url, server.Secret, request);
            if (response.ReturnCode == "SUCCESS")
            {
                var meeting = _mapper.Map<Entities.Meeting>(response);
                meeting.Running = true;
                server.Meetings.Add(meeting);
                await _appDbContext.SaveChangesAsync();
            }

            return Ok(response);
        }

        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Xml)]
        [ProducesResponseType(typeof(CreateResponse), 200)]
        public async Task<IActionResult> Create([FromQuery] CreateMeetingDto dto, [FromBody] Modules modules)
        {
            var existingMeeting = await _appDbContext.Meetings.FirstOrDefaultAsync(m => m.Running && m.MeetingID == dto.MeetingId);
            if (existingMeeting != null)
            {
                var earlyResponse = _mapper.Map<CreateResponse>(existingMeeting);
                earlyResponse.ReturnCode = "SUCCESS";
                earlyResponse.MessageKey = "duplicateWarning";
                earlyResponse.Message = "This conference was already in existence and may currently be in progress.";
                return Ok(earlyResponse);
            }

            var server = await GetAvailableServer();
            if (server == null)
            {
                return Ok(new BaseBBBResponse
                {
                    ReturnCode = "FAILED",
                    MessageKey = "unavailableServer",
                    Message = "[BigBlueBalancer] Unavailble server"
                });
            }

            var request = _mapper.Map<CreateRequest>(dto);
            var response = await _bbbClient.Create(server.Url, server.Secret, request, modules);
            if (response.ReturnCode == "SUCCESS")
            {
                var meeting = _mapper.Map<Entities.Meeting>(response);
                meeting.Running = true;
                server.Meetings.Add(meeting);
                await _appDbContext.SaveChangesAsync();
            }

            return Ok(response);
        }

        [HttpGet("join")]
        public async Task<IActionResult> Join([FromQuery] JoinMeetingDto dto)
        {
            var meeting = await GetMeeting(dto.MeetingID);
            if (meeting == null)
                return NotFound();

            var request = _mapper.Map<JoinRequest>(dto);
            var joinUrl = _bbbClient.GetJoinUrl(meeting.Server.Url, meeting.Server.Secret, request);
            return Redirect(joinUrl);
        }
    }
}
