using System;
using System.Collections.Generic;
using System.Text;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts value into Base64 using UTF-8 encoding.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ConvertToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Converts value into Base64Url using UTF-8 encoding.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ConvertToBase64Url(this string value)
        {
            var sb = new StringBuilder(ConvertToBase64(value));

            sb.Replace('+', '-');
            sb.Replace('/', '_');
            sb.Replace("=", string.Empty);

            return sb.ToString();
        }

        /// <summary>
        /// Splits value into a list of tokens of given length.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="tokenLength">The length of each token.</param>
        public static IEnumerable<string> SplitToTokensInLength(this string value, int tokenLength)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "String instance is null.");
            }

            if (tokenLength < 1)
            {
                throw new ArgumentException("Token length must be greater than or equal to 1.");
            }

            return Split();

            // Roslynator RCS1227
            IEnumerable<string> Split()
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    yield break;
                }

                if (value.Length < tokenLength)
                {
                    yield return value.PadRight(tokenLength);

                    yield break;
                }

                var tokenCount = Convert.ToInt32(Math.Floor(Convert.ToDouble(value.Length) / Convert.ToDouble(tokenLength)));

                var tokenIndex = 0;

                for (; tokenIndex < tokenCount; tokenIndex++)
                {
                    yield return value.Substring(tokenIndex * tokenLength, tokenLength);
                }

                var remainder = value.Length % tokenLength;

                if (remainder != 0)
                {
                    yield return value.Substring(tokenIndex * tokenLength, remainder).PadRight(tokenLength);
                }
            }
        }
    }
}
