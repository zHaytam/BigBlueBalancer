using BigBlueButton.Client.Models.Responses;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BigBlueButton.Client.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> DeserializeXml<T>(this HttpContent content) where T : BBBResponse
        {
            var serializer = new XmlSerializer(typeof(T));
            var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            return (T)serializer.Deserialize(stream);
        }
    }
}
