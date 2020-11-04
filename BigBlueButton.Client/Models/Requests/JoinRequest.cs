using BigBlueButton.Client.Parameters;

namespace BigBlueButton.Client.Models.Requests
{
    public class JoinRequest
    {
        [BBBParameter(Required = true)]
        public string FullName { get; set; }
        [BBBParameter(Required = true)]
        public string MeetingID { get; set; }
        [BBBParameter(Required = true)]
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string UserID { get; set; }
        public string WebVoiceConf { get; set; }
        public string ConfigToken { get; set; }
        public string DefaultLayout { get; set; }
        public string AvatarURL { get; set; }
        public bool Redirect { get; } = true;
        public string ClientURL { get; set; }
        public bool JoinViaHtml5 { get; } = true;
        public bool? Guest { get; set; }
    }
}
