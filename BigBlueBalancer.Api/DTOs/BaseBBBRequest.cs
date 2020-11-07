using System.ComponentModel.DataAnnotations;

namespace BigBlueBalancer.Api.DTOs
{
    public abstract class BaseBBBRequest
    {
        [Required]
        public string Checksum { get; set; }
    }
}
