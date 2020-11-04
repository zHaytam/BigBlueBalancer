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
        private readonly string _secret;
        private readonly ILogger<BBBClient> _logger;

        public BBBClient(HttpClient httpClient, string secret, ILogger<BBBClient> logger)
        {
            _httpClient = httpClient;
            _secret = secret;
            _logger = logger;
        }

        #region Administration

        public async Task<CreateResponse> Create(CreateRequest request)
            => await ExecuteRequest<CreateResponse>("create", ParametersExtractor.GenerateQueryString(request));

        public string GetJoinUrl(JoinRequest request)
            => ConstructUrl("join", ParametersExtractor.GenerateQueryString(request), true);

        #endregion

        #region Monitoring

        public async Task<GetMeetingsResponse> GetMeetings()
            => await ExecuteRequest<GetMeetingsResponse>("getMeetings");

        public async Task<GetMeetingInfoResponse> GetMeetingInfo(string meetingId)
            => await ExecuteRequest<GetMeetingInfoResponse>("getMeetingInfo", new Dictionary<string, string>
            {
                { "meetingID", meetingId }
            });

        #endregion

        private async Task<T> ExecuteRequest<T>(string callName, Dictionary<string, string> parameters = null) where T : BBBResponse
        {
            var query = parameters == null ? null : GenerateQueryString(parameters);
            return await ExecuteRequest<T>(callName, query).ConfigureAwait(false);
        }

        private string ConstructUrl(string callName, string query, bool full = false)
        {
            var checksum = CheksumGenerator.Generate(callName, _secret, query);
            var url = $"{callName}?{query}&checksum={checksum}";
            if (full) url = $"{_httpClient.BaseAddress}{url}";
            return url;
        }

        private async Task<T> ExecuteRequest<T>(string callName, string query) where T : BBBResponse
        {
            var url = ConstructUrl(callName, query);
            _logger.LogDebug($"{_httpClient.BaseAddress}{url}");
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            return await HandleResponse<T>(response).ConfigureAwait(false);
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response) where T : BBBResponse
        {
            using (response)
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.DeserializeXml<T>().ConfigureAwait(false);
            }
        }

        private static string GenerateQueryString(Dictionary<string, string> @params)
            => string.Join("&", @params.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));
    }
}
