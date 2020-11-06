using System;

namespace BigBlueBalancer.Api.DTOs.Servers
{
    public class ServerDto : BaseEntityDto
    {
        public short Id { get; set; }
        public string Url { get; set; }
        public string Secret { get; set; }

        public bool Up { get; set; }
        public int ParticipantCount { get; set; }
        public int ListenerCount { get; set; }
        public int VoiceParticipantCount { get; set; }
        public int VideoCount { get; set; }
        public DateTime? LastChecked { get; set; }
    }
}
