using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="FileInfo"/>
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Determines if the extension of the file is equal to the supplied value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="extension">Extension.</param>
        public static bool HasExtension(this FileInfo value, string extension)
        {
            return value.Extension.Equals(extension);
        }

        /// <summary>
        /// Determines if the extension of the file is equal to the supplied value using the provided <see cref="StringComparison"/>.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="extension">Extension.</param>
        /// <param name="stringComparison">One of the enumeration values that specifies how the strings will be compared.</param>
        public static bool HasExtension(this FileInfo value, string extension, StringComparison stringComparison)
        {
            return value.Extension.Equals(extension, stringComparison);
        }

        /// <summary>
        /// Determines if the extension of the file is equal any of the supplied values.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="extensions">An <see cref="IEnumerable{T}"/> of <see cref="string"/> values that represent the extensions.</param>
        public static bool HasAnyExtension(this FileInfo value, IEnumerable<string> extensions)
        {
            return extensions.Any(x => value.Extension.Equals(x));
        }

        /// <summary>
        /// Determines if the extension of the file is equal any of the supplied values using the provided <see cref="StringComparison"/>.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="extensions">An <see cref="IEnumerable{T}"/> of <see cref="string"/> values that represent the extensions.</param>
        /// <param name="stringComparison">One of the enumeration values that specifies how the strings will be compared.</param>
        public static bool HasAnyExtension(this FileInfo value, IEnumerable<string> extensions, StringComparison stringComparison)
        {
            return extensions.Any(x => value.Extension.Equals(x, stringComparison));
        }
    }
}
