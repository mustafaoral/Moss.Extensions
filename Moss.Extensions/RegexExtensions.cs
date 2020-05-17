using System.Text.RegularExpressions;

namespace Moss.Extensions
{
    public static class RegexExtensions
    {
        /// <summary>
        /// Tries to match the input using the Regex instance value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="input">The input to match.</param>
        /// <param name="match">Contains the match if the method returns true, null otherwise.</param>
        public static bool TryMatch(this Regex value, string input, out Match match)
        {
            var _match = value.Match(input);

            if (_match.Success)
            {
                match = _match;

                return true;
            }

            match = null;

            return false;
        }
    }
}
