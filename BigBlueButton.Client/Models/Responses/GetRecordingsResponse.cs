using System.Collections.Generic;
using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
    [XmlRoot(ElementName = "metadata")]
	public class RecordingMetadata
	{
		[XmlElement(ElementName = "isBreakout")]
		public bool IsBreakout { get; set; }
		[XmlElement(ElementName = "meetingName")]
		public string MeetingName { get; set; }
		[XmlElement(ElementName = "gl-listed")]
		public string Gllisted { get; set; }
		[XmlElement(ElementName = "meetingId")]
		public string MeetingId { get; set; }
	}

	[XmlRoot(ElementName = "format")]
	public class Format
	{
		[XmlElement(ElementName = "type")]
		public string Type { get; set; }
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
		[XmlElement(ElementName = "processingTime")]
		public long ProcessingTime { get; set; }
		[XmlElement(ElementName = "length")]
		public int Length { get; set; }
		[XmlElement("size")]
		public long Size { get; set; }
		[XmlElement(ElementName = "preview")]
		public Preview Preview { get; set; }
	}

	[XmlRoot(ElementName = "image")]
	public class Image
	{
		[XmlAttribute(AttributeName = "alt")]
		public string Alt { get; set; }
		[XmlAttribute(AttributeName = "height")]
		public string Height { get; set; }
		[XmlAttribute(AttributeName = "width")]
		public string Width { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "images")]
	public class Images
	{
		[XmlElement(ElementName = "image")]
		public List<Image> Items { get; set; }
	}

	[XmlRoot(ElementName = "preview")]
	public class Preview
	{
		[XmlElement(ElementName = "images")]
		public Images Images { get; set; }
	}

	[XmlRoot(ElementName = "playback")]
	public class Playback
	{
		[XmlElement(ElementName = "format")]
		public List<Format> Format { get; set; }
	}

	[XmlRoot(ElementName = "recording")]
	public class Recording
	{
		[XmlElement(ElementName = "recordID")]
		public string RecordID { get; set; }
		[XmlElement(ElementName = "meetingID")]
		public string MeetingID { get; set; }
		[XmlElement(ElementName = "internalMeetingID")]
		public string InternalMeetingID { get; set; }
		[XmlElement(ElementName = "name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "isBreakout")]
		public bool IsBreakout { get; set; }
		[XmlElement(ElementName = "published")]
		public bool Published { get; set; }
		[XmlElement(ElementName = "state")]
		public string State { get; set; }
		[XmlElement(ElementName = "startTime")]
		public long StartTime { get; set; }
		[XmlElement(ElementName = "endTime")]
		public long EndTime { get; set; }
		[XmlElement(ElementName = "participants")]
		public int Participants { get; set; }
		[XmlElement("size")]
		public long Size { get; set; }
		[XmlElement("rawSize")]
		public long RawSize { get; set; }
		[XmlElement(ElementName = "metadata")]
		public RecordingMetadata Metadata { get; set; }
		[XmlElement(ElementName = "playback")]
		public Playback Playback { get; set; }
	}

	[XmlRoot(ElementName = "recordings")]
	public class Recordings
	{
		[XmlElement(ElementName = "recording")]
		public List<Recording> Items { get; set; }
	}

	[XmlRoot(ElementName = "response")]
	public class GetRecordingsResponse : BBBResponse
	{
		[XmlElement(ElementName = "recordings")]
		public Recordings Recordings { get; set; }
	}
}
