using System.ComponentModel.DataAnnotations;

namespace BigBlueBalancer.Api.DTOs.Servers
{
    public class NewServerDto
    {
        [Url]
        [Required]
        public string Url { get; set; }
        [Required]
        public string Secret { get; set; }
    }
}
