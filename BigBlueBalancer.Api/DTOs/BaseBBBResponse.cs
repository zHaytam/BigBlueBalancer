using System.Xml.Serialization;

namespace BigBlueBalancer.Api.DTOs
{
    public class BaseBBBResponse
    {
        [XmlElement(ElementName = "returncode")]
        public string ReturnCode { get; set; }
        [XmlElement(ElementName = "messageKey")]
        public string MessageKey { get; set; }
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }

        public static BaseBBBResponse ChecksumError { get; } = new BaseBBBResponse
        {
            ReturnCode = "FAILED",
            MessageKey = "checksumError",
            Message = "You did not pass the checksum security check"
        };
    }
}
