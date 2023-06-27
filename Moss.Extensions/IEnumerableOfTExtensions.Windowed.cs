namespace Moss.Extensions;

public static partial class IEnumerableOfTExtensions
{
    /// <summary>
    /// Projects into sequences of windows given the window size and step.
    /// </summary>
    /// <typeparam name="T">F</typeparam>
    /// <param name="values">Values.</param>
    /// <param name="size">Size of window.</param>
    /// <param name="step">Step of each window.</param>
    /// <param name="partialWindows">Indicated whether or not to return partial windows, where the window size is less than the requested size</param>
    public static IEnumerable<IEnumerable<T>> Windowed<T>(this IEnumerable<T> values, int size, int step = 1, bool partialWindows = false)
    {
        if (values == null)
        {
            throw new ArgumentNullException(nameof(values), $"IEnumerable<{typeof(T).Name}> instance cannot be null");
        }

        if (size < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(size), size, "Size must be greater than zero");
        }

        if (step < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(step), step, "Step must be greater than zero");
        }

        var count = values.Count();
        var i = 0;

        if (size > count && !partialWindows)
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            while (i <= count - size)
            {
                yield return values.Skip(i).Take(size);

                i += step;
            }

            if (partialWindows)
            {
                while (i < count)
                {
                    yield return values.Skip(i).Take(size);

                    i += step;
                    size -= step;
                }
            }
        }
    }
}
