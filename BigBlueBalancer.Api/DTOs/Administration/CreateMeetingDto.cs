using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BigBlueBalancer.Api.DTOs.Administration
{
    public class CreateMeetingDto : BaseBBBRequest
    {
        public string Name { get; set; }
        [Required]
        [FromQuery(Name = "meetingID")]
        public string MeetingId { get; set; }
        [FromQuery(Name = "attendeePW")]
        public string AttendeePassword { get; set; }
        [FromQuery(Name = "moderatorPW")]
        public string ModeratorPassword { get; set; }
        public string Welcome { get; set; }
        public string DialNumber { get; set; }
        public string VoiceBridge { get; set; }
        public int? MaxParticipants { get; set; }
        public bool? Record { get; set; }
        public int? Duration { get; set; }
        public bool? IsBreakout { get; set; }
        [FromQuery(Name = "parentMeetingID")]
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

        [FromQuery(Name = "meta_endCallbackUrl")]
        public string EndMeetingCallbackUrl { get; set; }
        [FromQuery(Name = "meta_bbb-recording-ready-url")]
        public string RecordingReadyCallbackUrl { get; set; }
    }
}
