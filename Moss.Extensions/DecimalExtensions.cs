using System;
using System.Globalization;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="decimal"/>
    /// </summary>
    public static class DecimalExtensions
    {
        private static readonly CultureInfo _enGbCultureInfo = CultureInfo.GetCultureInfo("en-gb");

        /// <summary>
        /// Converts value into currency formatting using the supplied format provider.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="formatProvider">Format provider used in conversion.</param>
        public static string ToCurrencyString(this decimal value, IFormatProvider formatProvider)
        {
            return value.ToString("C", formatProvider);
        }

        /// <summary>
        /// Converts value to currency formatting using en-gb CultureInfo.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ToEnGbCurrencyString(this decimal value)
        {
            return value.ToString("C", _enGbCultureInfo);
        }
    }
}
