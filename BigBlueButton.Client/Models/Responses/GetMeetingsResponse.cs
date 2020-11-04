using System.Collections.Generic;
using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
    [XmlRoot(ElementName = "attendee")]
    public class Attendee
    {
        [XmlElement(ElementName = "userID")]
        public string UserID { get; set; }
        [XmlElement(ElementName = "fullName")]
        public string FullName { get; set; }
        [XmlElement(ElementName = "role")]
        public string Role { get; set; }
        [XmlElement(ElementName = "isPresenter")]
        public bool IsPresenter { get; set; }
        [XmlElement(ElementName = "isListeningOnly")]
        public bool IsListeningOnly { get; set; }
        [XmlElement(ElementName = "hasJoinedVoice")]
        public bool HasJoinedVoice { get; set; }
        [XmlElement(ElementName = "hasVideo")]
        public bool HasVideo { get; set; }
        [XmlElement(ElementName = "clientType")]
        public string ClientType { get; set; }
    }

    [XmlRoot(ElementName = "attendees")]
    public class Attendees
    {
        [XmlElement(ElementName = "attendee")]
        public List<Attendee> Items { get; set; }
    }

    [XmlRoot(ElementName = "metadata")]
    public class Metadata
    {
        [XmlElement(ElementName = "bbb-recording-ready-url")]
        public string BbbRecordingReadyUrl { get; set; }
        [XmlElement(ElementName = "endcallbackurl")]
        public string EndCallbackUrl { get; set; }
    }

    [XmlRoot(ElementName = "meeting")]
    public class Meeting
    {
        [XmlElement(ElementName = "meetingName")]
        public string MeetingName { get; set; }
        [XmlElement(ElementName = "meetingID")]
        public string MeetingID { get; set; }
        [XmlElement(ElementName = "internalMeetingID")]
        public string InternalMeetingID { get; set; }
        [XmlElement(ElementName = "createTime")]
        public long CreateTime { get; set; }
        [XmlElement(ElementName = "createDate")]
        public string CreateDate { get; set; }
        [XmlElement(ElementName = "voiceBridge")]
        public string VoiceBridge { get; set; }
        [XmlElement(ElementName = "dialNumber")]
        public string DialNumber { get; set; }
        [XmlElement(ElementName = "attendeePW")]
        public string AttendeePW { get; set; }
        [XmlElement(ElementName = "moderatorPW")]
        public string ModeratorPW { get; set; }
        [XmlElement(ElementName = "running")]
        public bool Running { get; set; }
        [XmlElement(ElementName = "duration")]
        public int Duration { get; set; }
        [XmlElement(ElementName = "hasUserJoined")]
        public bool HasUserJoined { get; set; }
        [XmlElement(ElementName = "recording")]
        public bool Recording { get; set; }
        [XmlElement(ElementName = "hasBeenForciblyEnded")]
        public bool HasBeenForciblyEnded { get; set; }
        [XmlElement(ElementName = "startTime")]
        public long StartTime { get; set; }
        [XmlElement(ElementName = "endTime")]
        public long EndTime { get; set; }
        [XmlElement(ElementName = "participantCount")]
        public int ParticipantCount { get; set; }
        [XmlElement(ElementName = "listenerCount")]
        public int ListenerCount { get; set; }
        [XmlElement(ElementName = "voiceParticipantCount")]
        public int VoiceParticipantCount { get; set; }
        [XmlElement(ElementName = "videoCount")]
        public int VideoCount { get; set; }
        [XmlElement(ElementName = "maxUsers")]
        public int MaxUsers { get; set; }
        [XmlElement(ElementName = "moderatorCount")]
        public int ModeratorCount { get; set; }
        [XmlElement(ElementName = "attendees")]
        public Attendees Attendees { get; set; }
        [XmlElement(ElementName = "metadata")]
        public Metadata Metadata { get; set; }
        [XmlElement(ElementName = "isBreakout")]
        public bool IsBreakout { get; set; }
    }

    [XmlRoot(ElementName = "meetings")]
    public class Meetings
    {
        [XmlElement(ElementName = "meeting")]
        public List<Meeting> Items { get; set; }
    }

    [XmlRoot(ElementName = "response")]
    public class GetMeetingsResponse : BBBResponse
    {
        [XmlElement(ElementName = "meetings")]
        public Meetings Meetings { get; set; }
    }
}
