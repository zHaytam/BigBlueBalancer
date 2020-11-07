namespace BigBlueBalancer.Api.DTOs.Servers
{
    public class ServerStatsDto
    {
        public bool Up { get; set; }
        public int MeetingsCount { get; set; }
        public int ParticipantCount { get; set; }
        public int ListenerCount { get; set; }
        public int VoiceParticipantCount { get; set; }
        public int VideoCount { get; set; }
    }
}
