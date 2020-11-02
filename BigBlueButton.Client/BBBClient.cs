using BigBlueButton.Client.Extensions;
using BigBlueButton.Client.Models;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Client
{
    public class BBBClient : IBBBClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _secret;

        public BBBClient(HttpClient httpClient, string secret)
        {
            _httpClient = httpClient;
            _secret = secret;
        }

        public async Task<GetMeetingsResponse> GetMeetings()
        {
            var checksum = GenerateChecksum("getMeetings");
            using (var response = await _httpClient.GetAsync($"getMeetings?checksum={checksum}").ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.DeserializeXml<GetMeetingsResponse>().ConfigureAwait(false);
            }
        }
    }
}
