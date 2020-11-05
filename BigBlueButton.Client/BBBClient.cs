using BigBlueButton.Client.Extensions;
using BigBlueButton.Client.Helpers;
using BigBlueButton.Client.Models.Requests;
using BigBlueButton.Client.Models.Responses;
using BigBlueButton.Client.Parameters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<CreateResponse> Create(string baseUrl, string secret, CreateRequest request)
            => await ExecuteRequest<CreateResponse>(baseUrl, secret, "create", ParametersExtractor.GenerateQueryString(request));

        public string GetJoinUrl(string baseUrl, string secret, JoinRequest request)
            => ConstructUrl(baseUrl, secret, "join", ParametersExtractor.GenerateQueryString(request), true);

        #endregion

        #region Monitoring

        public async Task<GetMeetingsResponse> GetMeetings(string baseUrl, string secret)
            => await ExecuteRequest<GetMeetingsResponse>(baseUrl, secret, "getMeetings");

        public async Task<GetMeetingInfoResponse> GetMeetingInfo(string baseUrl, string secret, string meetingId)
            => await ExecuteRequest<GetMeetingInfoResponse>(baseUrl, secret, "getMeetingInfo",
                new Dictionary<string, string>
            {
                { "meetingID", meetingId }
            });

        #endregion

        private string ConstructUrl(string baseUrl, string secret, string callName, string query, bool full = false)
        {
            var checksum = CheksumGenerator.Generate(callName, secret, query);
            var url = $"{baseUrl}{callName}?{query}&checksum={checksum}";
            if (full) url = $"{_httpClient.BaseAddress}{url}";
            return url;
        }

        private async Task<T> ExecuteRequest<T>(string baseUrl, string secret, string callName,
            Dictionary<string, string> parameters = null) where T : BBBResponse
        {
            var query = parameters == null ? null : GenerateQueryString(parameters);
            return await ExecuteRequest<T>(baseUrl, secret, callName, query).ConfigureAwait(false);
        }

        private async Task<T> ExecuteRequest<T>(string baseUrl, string secret, string callName, string query) where T : BBBResponse
        {
            var url = ConstructUrl(baseUrl, secret, callName, query);
            _logger.LogDebug($"{url}");

            using var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.DeserializeXml<T>().ConfigureAwait(false);
        }

        private static string GenerateQueryString(Dictionary<string, string> @params)
            => string.Join("&", @params.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));
    }
}
