using System;

namespace BigBlueBalancer.Api.DTOs
{
    public class BaseEntityDto
    {
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
