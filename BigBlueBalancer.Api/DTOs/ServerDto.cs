namespace BigBlueBalancer.Api.DTOs
{
    public class ServerDto : BaseEntityDto
    {
        public short Id { get; set; }
        public string Url { get; set; }
        public string Secret { get; set; }
    }
}
