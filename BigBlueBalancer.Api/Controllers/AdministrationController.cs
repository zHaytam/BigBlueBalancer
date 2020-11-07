﻿using AutoMapper;
using BigBlueBalancer.Api.DTOs;
using BigBlueBalancer.Api.DTOs.Administration;
using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [Route("bigbluebutton/api")]
    public class AdministrationController : BBBController
    {
        private readonly IBBBClient _bbbClient;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AdministrationController(IBBBClient bbbClient, AppDbContext appDbContext, IMapper mapper, 
            IConfiguration configuration) : base(appDbContext, configuration)
        {
            _bbbClient = bbbClient;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create([FromQuery] CreateMeetingDto dto)
        {
            var request = _mapper.Map<CreateRequest>(dto);
            if (!IsChecksumValid("create", request, dto.Checksum))
                return Ok(BaseBBBResponse.ChecksumError);

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
                return Ok(BaseBBBResponse.UnavailableServer);

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
    }
}