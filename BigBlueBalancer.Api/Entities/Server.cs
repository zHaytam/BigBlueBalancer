using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace BigBlueBalancer.Api.Entities
{
    public class Server : BaseEntity
    {
        public short Id { get; set; }
        public string Url { get; set; }
        public string Secret { get; set; }
        public bool Up { get; set; }
        public double Load { get; set; }

        public List<ServerStats> Stats { get; set; } = new List<ServerStats>();
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();
    }

    public class ServerConfiguration : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            builder.Property(p => p.Url).IsRequired();
            builder.Property(p => p.Secret).IsRequired();
        }
    }
}
