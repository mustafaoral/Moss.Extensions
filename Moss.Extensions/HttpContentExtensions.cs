using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HttpContent"/>
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Reads the content as stream and deserializes it using <see cref="JsonSerializer"/>.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="jsonSerializerOptions">Options to control the behavior during reading.</param>
        public static async Task<T> DeserializeAsStreamAsync<T>(this HttpContent value, JsonSerializerOptions jsonSerializerOptions = null)
        {
            return await JsonSerializer.DeserializeAsync<T>(await value.ReadAsStreamAsync().ConfigureAwait(false), jsonSerializerOptions).ConfigureAwait(false);
        }
    }
}
