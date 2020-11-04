using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
	[XmlRoot(ElementName = "response")]
	public class JoinResponse : BBBResponse
    {
		[XmlElement(ElementName = "meeting_id")]
		public string MeetingId { get; set; }
		[XmlElement(ElementName = "user_id")]
		public string UserId { get; set; }
		[XmlElement(ElementName = "auth_token")]
		public string AuthToken { get; set; }
		[XmlElement(ElementName = "session_token")]
		public string SessionToken { get; set; }
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
	}
}
