using System.Collections.Generic;
using System.Linq;

namespace Moss.Extensions
{
    public static class IEnumerableOfByteExtensions
    {
        /// <summary>
        /// Converts each byte in the array to its uppercase hex string representation, joining tokens using empty string separator.
        /// </summary>
        /// <param name="values">Values</param>
        public static string ToHexString(this IEnumerable<byte> values)
        {
            return ToHexString(values, string.Empty);
        }

        /// <summary>
        /// Converts each byte in the array to its uppercase hex string representation, joining tokens using provided separator.
        /// </summary>
        /// <param name="values">Values</param>
        /// <param name="separator">Separator to use for joining.</param>
        public static string ToHexString(this IEnumerable<byte> values, string separator)
        {
            return string.Join(separator, values.Select(x => x.ToString("X2")));
        }
    }
}
