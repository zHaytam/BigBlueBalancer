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
            var response = await _httpClient.GetAsync($"getMeetings?checksum={checksum}");
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(GetMeetingsResponse));
            return (GetMeetingsResponse)serializer.Deserialize(responseStream);
        }

        private string GenerateChecksum(string callName, string query = null)
        {
            using (var sha1 = SHA1.Create())
            {
                var result = sha1.ComputeHash(Encoding.ASCII.GetBytes($"{callName}{query}{_secret}"));
                return BitConverter.ToString(result).Replace("-", "").ToLower();
            }
        }
    }
}
