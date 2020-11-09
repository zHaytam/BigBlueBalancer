using BigBlueButton.Client.Parameters;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Requests
{
    public class CreateRequest
    {
        public string Name { get; set; }
        [BBBParameter("meetingID", required: true)]
        public string MeetingId { get; set; }
        [BBBParameter("attendeePW")]
        public string AttendeePassword { get; set; }
        [BBBParameter("moderatorPW")]
        public string ModeratorPassword { get; set; }
        public string Welcome { get; set; }
        public string DialNumber { get; set; }
        public string VoiceBridge { get; set; }
        public int? MaxParticipants { get; set; }
        public string LogoutURL { get; set; }
        public bool? Record { get; set; }
        public int? Duration { get; set; }
        public bool? IsBreakout { get; set; }
        [BBBParameter("parentMeetingID")]
        public string ParentMeetingId { get; set; }
        public int? Sequence { get; set; }
        public bool? FreeJoin { get; set; }
        public bool? AutoStartRecording { get; set; }
        public bool? AllowStartStopRecording { get; set; }
        public bool? WebcamsOnlyForModerator { get; set; }
        public string Logo { get; set; }
        public string BannerText { get; set; }
        public string BannerColor { get; set; }
        public string Copyright { get; set; }
        public bool? MuteOnStart { get; set; }
        public bool? AllowModsToUnmuteUsers { get; set; }
        public bool? LockSettingsDisableCam { get; set; }
        public bool? LockSettingsDisableMic { get; set; }
        public bool? LockSettingsDisablePrivateChat { get; set; }
        public bool? LockSettingsDisablePublicChat { get; set; }
        public bool? LockSettingsDisableNote { get; set; }
        public bool? LockSettingsLockedLayout { get; set; }
        public bool? LockSettingsLockOnJoin { get; set; }
        public bool? LockSettingsLockOnJoinConfigurable { get; set; }
        public string GuestPolicy { get; set; }

        [BBBParameter("meta_endCallbackUrl")]
        public string EndMeetingCallbackUrl { get; set; }
        [BBBParameter("meta_bbb-recording-ready-url")]
        public string RecordingReadyCallbackUrl { get; set; }

        public CreateRequest(string meetingId)
        {
            MeetingId = meetingId;
        }
    }

    [XmlRoot(ElementName = "document")]
    public class Document
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
        [XmlAttribute(AttributeName = "filename")]
        public string Filename { get; set; }
    }

    [XmlRoot(ElementName = "module")]
    public class Module
    {
        [XmlElement(ElementName = "document")]
        public List<Document> Documents { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "modules")]
    public class Modules
    {
        [XmlElement(ElementName = "module")]
        public Module Module { get; set; }
    }
}
