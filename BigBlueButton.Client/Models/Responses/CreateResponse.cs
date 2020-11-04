using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
	[XmlRoot(ElementName = "response")]
	public class CreateResponse : BBBResponse
    {
		[XmlElement(ElementName = "meetingID")]
		public string MeetingID { get; set; }
		[XmlElement(ElementName = "internalMeetingID")]
		public string InternalMeetingID { get; set; }
		[XmlElement(ElementName = "parentMeetingID")]
		public string ParentMeetingID { get; set; }
		[XmlElement(ElementName = "attendeePW")]
		public string AttendeePW { get; set; }
		[XmlElement(ElementName = "moderatorPW")]
		public string ModeratorPW { get; set; }
		[XmlElement(ElementName = "createTime")]
		public string CreateTime { get; set; }
		[XmlElement(ElementName = "voiceBridge")]
		public string VoiceBridge { get; set; }
		[XmlElement(ElementName = "dialNumber")]
		public string DialNumber { get; set; }
		[XmlElement(ElementName = "createDate")]
		public string CreateDate { get; set; }
		[XmlElement(ElementName = "hasUserJoined")]
		public bool HasUserJoined { get; set; }
		[XmlElement(ElementName = "duration")]
		public int Duration { get; set; }
		[XmlElement(ElementName = "hasBeenForciblyEnded")]
		public bool HasBeenForciblyEnded { get; set; }
	}
}
