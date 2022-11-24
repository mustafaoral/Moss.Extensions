using System;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly string _isoDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Converts value to ISO 8601 like representation made up of years, months, and days parts.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ToIso8601DateString(this DateTime value)
        {
            return value.ToString(_isoDateFormat);
        }
    }
}
