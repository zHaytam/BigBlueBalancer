using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
    [XmlRoot(ElementName = "response")]
    public class GetMeetingInfoResponse : BBBResponse
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
}
