using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Send a GET request to the specified Uri and reads the response content as stream and deserializes it using <see cref="JsonSerializer"/>.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="jsonSerializerOptions">Options to control the behavior during reading.</param>
        public static async Task<T> GetAsStreamAndDeserializeAsync<T>(this HttpClient value, string requestUri, JsonSerializerOptions jsonSerializerOptions = null)
        {
            using (var response = await value.GetStreamAsync(requestUri).ConfigureAwait(false))
            {
                return await JsonSerializer.DeserializeAsync<T>(response, jsonSerializerOptions).ConfigureAwait(false);
            }
        }
    }
}
