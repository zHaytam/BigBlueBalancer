using System;

namespace BigBlueBalancer.Api.Entities
{
    public class BaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
