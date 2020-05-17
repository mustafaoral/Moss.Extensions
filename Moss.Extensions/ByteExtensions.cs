using System;

namespace Moss.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Convert value to uppercase hex representation.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ToHexString(this byte value)
        {
            return value.ToString("X2");
        }

        /// <summary>
        /// Converts value to a string representation of its bits using least significant bit ordering.
        /// </summary>
        /// <param name="value">Value.</param>
        public static string ToBitString(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        /// <summary>
        /// Converts value to a char array representation of its bits using least significant bit ordering.
        /// </summary>
        /// <param name="value">Value.</param>
        public static char[] ToBitChars(this byte value)
        {
            return ToBitString(value).ToCharArray();
        }
    }
}
