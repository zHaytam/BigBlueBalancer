using System.ComponentModel.DataAnnotations;

namespace BigBlueBalancer.Api.DTOs.Servers
{
    public class CreateOrEditServerDto
    {
        [Url]
        [Required]
        public string Url { get; set; }
        [Required]
        public string Secret { get; set; }
    }
}
