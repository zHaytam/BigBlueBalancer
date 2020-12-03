using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    public class RecordingController : BBBController
    {
        private readonly IBBBClient _bbbClient;

        public RecordingController(IBBBClient bbbClient, AppDbContext appDbContext) : base(appDbContext)
        {
            _bbbClient = bbbClient;
        }

        [HttpGet("getRecordings")]
        [ProducesResponseType(typeof(GetRecordingsResponse), 200)]
        public async Task<GetRecordingsResponse> GetRecordings(string meetingID, string recordID)
        {
            var response = new GetRecordingsResponse
            {
                ReturnCode = "SUCCESS",
                Recordings = new Recordings
                {
                    Items = new List<Recording>()
                }
            };
            var servers = (await GetAvailableServers()).ToDictionary(s => s.Id, s => s);

            if (!string.IsNullOrEmpty(recordID))
            {
                var ids = recordID.Split(',');
                var meetings = await AppDbContext.Meetings.Where(m => ids.Contains(m.InternalMeetingID)).ToListAsync();
                var meetingsByServer = meetings.GroupBy(m => m.ServerId);

                foreach (var group in meetingsByServer)
                {
                    if (!servers.ContainsKey(group.Key))
                        continue;

                    var server = servers[group.Key];
                    var request = new GetRecordingsRequest
                    {
                        RecordId = string.Join(',', group.Select(m => m.InternalMeetingID))
                    };
                    var recordings = await _bbbClient.GetRecordings(server.Url, server.Secret, request);
                    if (recordings.Recordings?.Items?.Count > 0)
                    {
                        response.Recordings.Items.AddRange(recordings.Recordings.Items);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(meetingID))
            {
                var ids = meetingID.Split(',');
                var meetings = await AppDbContext.Meetings.Where(m => ids.Contains(m.MeetingID)).ToListAsync();
                var meetingsByServer = meetings.GroupBy(m => m.ServerId);

                foreach (var group in meetingsByServer)
                {
                    if (!servers.ContainsKey(group.Key))
                        continue;

                    var server = servers[group.Key];
                    var request = new GetRecordingsRequest
                    {
                        MeetingId = string.Join(',', group.Select(m => m.MeetingID))
                    };
                    var recordings = await _bbbClient.GetRecordings(server.Url, server.Secret, request);
                    if (recordings.Recordings?.Items?.Count > 0)
                    {
                        response.Recordings.Items.AddRange(recordings.Recordings.Items);
                    }
                }
            }
            else // Get all
            {
                var request = new GetRecordingsRequest();
                foreach ((var serverId, var server) in servers)
                {
                    var recordings = await _bbbClient.GetRecordings(server.Url, server.Secret, request);
                    if (recordings.Recordings?.Items?.Count > 0)
                    {
                        response.Recordings.Items.AddRange(recordings.Recordings.Items);
                    }
                }
            }

            if (response.Recordings.Items.Count == 0)
            {
                response.MessageKey = "noRecordings";
                response.Message = "There are no recordings for the meeting(s).";
            }

            return response;
        }
    }
}
