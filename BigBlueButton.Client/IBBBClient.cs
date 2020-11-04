using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using System.Threading.Tasks;

namespace BigBlueButton.Client
{
    public interface IBBBClient
    {
        Task<CreateResponse> Create(CreateRequest request);

        Task<GetMeetingsResponse> GetMeetings();
        Task<GetMeetingInfoResponse> GetMeetingInfo(string meetingId);
    }
}