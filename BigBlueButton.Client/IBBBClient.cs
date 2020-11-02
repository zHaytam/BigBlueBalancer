using BigBlueButton.Client.Models;
using System.Threading.Tasks;

namespace BigBlueButton.Client
{
    public interface IBBBClient
    {
        Task<GetMeetingsResponse> GetMeetings();
    }
}