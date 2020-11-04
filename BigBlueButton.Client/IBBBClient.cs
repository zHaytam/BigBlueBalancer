using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using System.Threading.Tasks;

namespace BigBlueButton.Client
{
    public interface IBBBClient
    {
        public Task<CreateResponse> Create(CreateRequest request);
        public string GetJoinUrl(JoinRequest request);

        public Task<GetMeetingsResponse> GetMeetings();
        public Task<GetMeetingInfoResponse> GetMeetingInfo(string meetingId);
    }
}