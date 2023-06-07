namespace Moss.Extensions;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/> of <see cref="string"/>
/// </summary>
public static class IEnumerableOfStringExtensions
{
    /// <summary>
    /// Joins items using a specified separator.
    /// </summary>
    /// <param name="values">Values.</param>
    /// <param name="separator">Separator used to join items.</param>
    public static string JoinWith(this IEnumerable<string> values, string separator)
    {
        return string.Join(separator, values);
    }

    /// <summary>
    /// Joins items using new line.
    /// </summary>
    /// <param name="values">Values.</param>
    public static string JoinWithNewLine(this IEnumerable<string> values)
    {
        return string.Join(Environment.NewLine, values);
    }

    /// <summary>
    /// Joins items using space.
    /// </summary>
    /// <param name="values">Values.</param>
    public static string JoinWithSpace(this IEnumerable<string> values)
    {
        return string.Join(" ", values);
    }

    /// <summary>
    /// Joins items using comma.
    /// </summary>
    /// <param name="values">Values.</param>
    public static string JoinWithComma(this IEnumerable<string> values)
    {
        return string.Join(",", values);
    }
}
