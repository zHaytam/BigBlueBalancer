using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BigBlueBalancer.Api.DTOs.Administration
{
    public class JoinMeetingDto : BaseBBBRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string MeetingID { get; set; }
        [Required]
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string UserID { get; set; }
        public string WebVoiceConf { get; set; }
        public string ConfigToken { get; set; }
        public string DefaultLayout { get; set; }
        public string AvatarURL { get; set; }
        [DefaultValue(true)]
        public bool Redirect { get; } = true;
        public string ClientURL { get; set; }
        [DefaultValue(true)]
        public bool JoinViaHtml5 { get; } = true;
        public bool? Guest { get; set; }
    }
}
