using BigBlueButton.Client.Parameters;

namespace BigBlueButton.Client.Models.Requests
{
    public class GetRecordingsRequest
    {
        [BBBParameter("meetingID")]
        public string MeetingId { get; set; }
        [BBBParameter("recordID")]
        public string RecordId { get; set; }
    }
}
