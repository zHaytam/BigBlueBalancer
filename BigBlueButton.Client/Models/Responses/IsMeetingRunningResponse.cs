using System.Xml.Serialization;

namespace BigBlueButton.Client.Models.Responses
{
    [XmlRoot(ElementName = "response")]
    public class IsMeetingRunningResponse : BBBResponse
    {
        [XmlElement(ElementName = "running")]
        public string Running { get; set; }
    }
}
