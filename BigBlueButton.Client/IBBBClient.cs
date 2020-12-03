using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using System.Threading.Tasks;

namespace BigBlueButton.Client
{
    public interface IBBBClient
    {
        public Task<CreateResponse> Create(string baseUrl, string secret, CreateRequest request, Modules modules = null);
        public string GetJoinUrl(string baseUrl, string secret, JoinRequest request);

        public Task<IsMeetingRunningResponse> IsMeetingRunning(string baseUrl, string secret, string meetingId);
        public Task<GetMeetingsResponse> GetMeetings(string baseUrl, string secret);
        public Task<GetMeetingInfoResponse> GetMeetingInfo(string baseUrl, string secret, string meetingId);

        public Task<GetRecordingsResponse> GetRecordings(string baseUrl, string secret, GetRecordingsRequest request);
    }
}