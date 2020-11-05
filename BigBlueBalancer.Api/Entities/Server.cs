using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBlueBalancer.Api.Entities
{
    public class Server : BaseEntity
    {
        public short Id { get; set; }
        public string Url { get; set; }
        public string Secret { get; set; }
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
