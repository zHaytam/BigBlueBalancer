namespace BigBlueBalancer.Api.Entities
{
    public class ServerStats : BaseEntity
    {
        public int Id { get; set; }
        public short ServerId { get; set; }

        public bool Up { get; set; }
        public int MeetingsCount { get; set; }
        public int ParticipantCount { get; set; }
        public int ListenerCount { get; set; }
        public int VoiceParticipantCount { get; set; }
        public int VideoCount { get; set; }

        public Server Server { get; set; }
    }
}
