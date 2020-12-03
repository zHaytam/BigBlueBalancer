using BigBlueButton.Client.Extensions;
using BigBlueButton.Client.Helpers;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using BigBlueButton.Client.Parameters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace BigBlueButton.Client
{
    public class BBBClient : IBBBClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BBBClient> _logger;

        public BBBClient(HttpClient httpClient, ILogger<BBBClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        #region Administration

        public async Task<CreateResponse> Create(string baseUrl, string secret, CreateRequest request, Modules modules = null)
        {
            HttpContent body = null;
            if (modules != null)
            {
                using var stringWriter = new StringWriter();
                var ser = new XmlSerializer(typeof(Modules));
                ser.Serialize(stringWriter, modules);
                body = new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/xml");
            }

            return await ExecuteRequest<CreateResponse>(baseUrl, secret, "create", 
                ParametersExtractor.GenerateQueryString(request), body);
        }

        public string GetJoinUrl(string baseUrl, string secret, JoinRequest request)
            => ConstructUrl(baseUrl, secret, "join", ParametersExtractor.GenerateQueryString(request));

        #endregion

        #region Monitoring

        public async Task<IsMeetingRunningResponse> IsMeetingRunning(string baseUrl, string secret, string meetingId)
            => await ExecuteRequest<IsMeetingRunningResponse>(baseUrl, secret, "isMeetingRunning", 
                new Dictionary<string, string>
            {
                { "meetingID", meetingId }
            });

        public async Task<GetMeetingsResponse> GetMeetings(string baseUrl, string secret)
            => await ExecuteRequest<GetMeetingsResponse>(baseUrl, secret, "getMeetings");

        public async Task<GetMeetingInfoResponse> GetMeetingInfo(string baseUrl, string secret, string meetingId)
            => await ExecuteRequest<GetMeetingInfoResponse>(baseUrl, secret, "getMeetingInfo",
                new Dictionary<string, string>
            {
                { "meetingID", meetingId }
            });

        #endregion

        private string ConstructUrl(string baseUrl, string secret, string callName, string query)
        {
            var checksum = ChecksumGenerator.Generate(callName, secret, query);
            return $"{baseUrl}/api/{callName}?{query}&checksum={checksum}";
        }

        private async Task<T> ExecuteRequest<T>(string baseUrl, string secret, string callName,
            Dictionary<string, string> parameters = null) where T : BBBResponse
        {
            var query = parameters == null ? null : GenerateQueryString(parameters);
            return await ExecuteRequest<T>(baseUrl, secret, callName, query).ConfigureAwait(false);
        }

        private async Task<T> ExecuteRequest<T>(string baseUrl, string secret, string callName, string query, 
            HttpContent body = null) where T : BBBResponse
        {
            var url = ConstructUrl(baseUrl, secret, callName, query);
            _logger.LogDebug($"{url}");

            using var response = await (body == null
                ? _httpClient.GetAsync(url)
                : _httpClient.PostAsync(url, body)).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.DeserializeXml<T>().ConfigureAwait(false);
        }

        private static string GenerateQueryString(Dictionary<string, string> @params)
            => string.Join("&", @params.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));
    }
}
