namespace Moss.Extensions;

/// <summary>
/// Extension methods for <see cref="int"/>
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// Gets the number of digits
    /// </summary>
    /// <param name="value">Value</param>
    /// <remarks></remarks>
    public static int GetNumberOfDigits(this int value)
    {
        if (value == int.MinValue)
        {
            return 10;
        }

        return Convert.ToInt32(Math.Floor(Math.Log10(Math.Abs(value)) + 1));
    }
}
