using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBlueBalancer.Api.Entities
{
    public class Meeting : BaseEntity
    {
        public int Id { get; set; }
		public short ServerId { get; set; }

		public string MeetingID { get; set; }
		public string InternalMeetingID { get; set; }
		public string ParentMeetingID { get; set; }
		public string AttendeePW { get; set; }
		public string ModeratorPW { get; set; }
		public string CreateTime { get; set; }
		public string VoiceBridge { get; set; }
		public string DialNumber { get; set; }
		public string CreateDate { get; set; }
		public bool HasUserJoined { get; set; }
		public int Duration { get; set; }
		public bool HasBeenForciblyEnded { get; set; }
		public bool Running { get; set; }

		public Server Server { get; set; }
	}

	public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
			builder.Property(p => p.MeetingID).IsRequired(true);
			builder.Property(p => p.InternalMeetingID).IsRequired(true);
			builder.Property(p => p.ParentMeetingID).IsRequired(true);
			builder.Property(p => p.AttendeePW).IsRequired(true);
			builder.Property(p => p.ModeratorPW).IsRequired(true);
			builder.Property(p => p.CreateTime).IsRequired(true);
			builder.Property(p => p.VoiceBridge).IsRequired(true);
			builder.Property(p => p.DialNumber).IsRequired(true);
			builder.Property(p => p.CreateDate).IsRequired(true);

			builder.HasIndex(p => p.MeetingID);
        }
    }
}
