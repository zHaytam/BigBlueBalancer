using System.Xml.Serialization;

namespace BigBlueButton.Client.Models
{
    public class BBBResponse
    {

        [XmlElement(ElementName = "returncode")]
        public string Returncode { get; set; }
        [XmlElement(ElementName = "messageKey")]
        public string MessageKey { get; set; }
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }
    }
}
