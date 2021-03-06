﻿using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    public class MonitoringController : BBBController
    {
        private readonly IBBBClient _bbbClient;
        private readonly ILogger<MonitoringController> _logger;

        public MonitoringController(IBBBClient bbbClient, AppDbContext appDbContext,
            ILogger<MonitoringController> logger) : base(appDbContext)
        {
            _bbbClient = bbbClient;
            _logger = logger;
        }

        [HttpGet("isMeetingRunning")]
        public async Task<IActionResult> IsMeetingRunning([Required] string meetingID)
        {
            var meeting = await GetMeeting(meetingID);
            if (meeting == null || !meeting.Server.Up)
            {
                _logger.LogWarning($"Meeting '{meetingID}' not found or it's server is down.");
                return Ok(new IsMeetingRunningResponse
                {
                    ReturnCode = "SUCCESS",
                    Running = "false"
                });
            }

            return Ok(await _bbbClient.IsMeetingRunning(meeting.Server.Url, meeting.Server.Secret, meetingID));
        }

        [HttpGet("getMeetings")]
        public async Task<GetMeetingsResponse> GetMeetings()
        {
            var response = new GetMeetingsResponse
            {
                ReturnCode = "SUCCESS",
                Meetings = new Meetings
                {
                    Items = new List<BigBlueButton.Client.Models.Responses.Meeting>()
                }
            };

            foreach (var server in await GetAvailableServers())
            {
                try
                {
                    var resp = await _bbbClient.GetMeetings(server.Url, server.Secret);
                    response.Meetings.Items.AddRange(resp.Meetings.Items);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Error while fetching meetings from '{server.Url}'.");
                }
            }

            if (response.Meetings.Items.Count == 0)
            {
                response.MessageKey = "noMeetings";
                response.Message = "no meetings were found on this server";
            }

            return response;
        }

        [HttpGet("getMeetingInfo")]
        public async Task<GetMeetingInfoResponse> GetMeetingInfo([Required] string meetingID)
        {
            var meeting = await GetMeeting(meetingID);
            if (meeting == null || !meeting.Server.Up)
            {
                _logger.LogWarning($"Meeting '{meetingID}' not found or it's server is down.");
                return new GetMeetingInfoResponse
                {
                    ReturnCode = "FAILED",
                    MessageKey = "notFound",
                    Message = "We could not find a meeting with that meeting ID"
                };
            }

            return await _bbbClient.GetMeetingInfo(meeting.Server.Url, meeting.Server.Secret, meetingID);
        }
    }
}
