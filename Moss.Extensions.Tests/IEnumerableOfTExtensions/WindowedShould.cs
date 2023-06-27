namespace Moss.Extensions.Tests.IEnumerableOfTExtensions;

public class WindowedShould
{
    private static readonly List<char> _values = new() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l' };

    [Fact]
    public void ThrowArgumentNullExceptionWhenInstanceIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => (null as List<int>).Windowed(size: 1).ToArray());

        exception.Message.ShouldContain("IEnumerable<Int32> instance cannot be null");
    }

    [Fact]
    public void ThrowArgumentOutOfRangeExceptionWhenSizeIsNotGreaterThanZero()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _values.Windowed(size: -1).ToArray());

        exception.Message.ShouldContain("Size must be greater than zero");
    }

    [Fact]
    public void ThrowArgumentOutOfRangeExceptionWhenStepIsNotGreaterThanZero()
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _values.Windowed(size: 1, step: -1).ToArray());

        exception.Message.ShouldContain("Step must be greater than zero");
    }

    [Theory]
    [ClassData(typeof(FullWindow))]
    public void ReturnCorrectSequenceForFullWindow(int size, int step, string flattenedSequence)
    {
        var result = _values.Windowed(size, step, partialWindows: false);

        $"[{result.Select(x => $"[{x.JoinWith(", ")}]").JoinWith(", ")}]".ShouldBe(flattenedSequence);
    }

    [Theory]
    [ClassData(typeof(PartialWindow))]
    public void ReturnCorrectSequenceForPartialWindow(int size, int step, string flattenedSequence)
    {
        var result = _values.Windowed(size, step, partialWindows: true);

        $"[{result.Select(x => $"[{x.JoinWith(", ")}]").JoinWith(", ")}]".ShouldBe(flattenedSequence);
    }

    private class FullWindow : TheoryData<int, int, string>
    {
        public FullWindow()
        {
            Add(1, 1, "[[a], [b], [c], [d], [e], [f], [g], [h], [i], [j], [k], [l]]");
            Add(1, 2, "[[a], [c], [e], [g], [i], [k]]");
            Add(1, 3, "[[a], [d], [g], [j]]");
            Add(1, 4, "[[a], [e], [i]]");
            Add(1, 5, "[[a], [f], [k]]");
            Add(1, 6, "[[a], [g]]");
            Add(1, 7, "[[a], [h]]");
            Add(1, 8, "[[a], [i]]");
            Add(1, 9, "[[a], [j]]");
            Add(1, 10, "[[a], [k]]");
            Add(1, 11, "[[a], [l]]");
            Add(1, 12, "[[a]]");

            Add(2, 1, "[[a, b], [b, c], [c, d], [d, e], [e, f], [f, g], [g, h], [h, i], [i, j], [j, k], [k, l]]");
            Add(2, 2, "[[a, b], [c, d], [e, f], [g, h], [i, j], [k, l]]");
            Add(2, 3, "[[a, b], [d, e], [g, h], [j, k]]");
            Add(2, 4, "[[a, b], [e, f], [i, j]]");
            Add(2, 5, "[[a, b], [f, g], [k, l]]");
            Add(2, 6, "[[a, b], [g, h]]");
            Add(2, 7, "[[a, b], [h, i]]");
            Add(2, 8, "[[a, b], [i, j]]");
            Add(2, 9, "[[a, b], [j, k]]");
            Add(2, 10, "[[a, b], [k, l]]");
            Add(2, 11, "[[a, b]]");
            Add(2, 12, "[[a, b]]");

            Add(3, 1, "[[a, b, c], [b, c, d], [c, d, e], [d, e, f], [e, f, g], [f, g, h], [g, h, i], [h, i, j], [i, j, k], [j, k, l]]");
            Add(3, 2, "[[a, b, c], [c, d, e], [e, f, g], [g, h, i], [i, j, k]]");
            Add(3, 3, "[[a, b, c], [d, e, f], [g, h, i], [j, k, l]]");
            Add(3, 4, "[[a, b, c], [e, f, g], [i, j, k]]");
            Add(3, 5, "[[a, b, c], [f, g, h]]");
            Add(3, 6, "[[a, b, c], [g, h, i]]");
            Add(3, 7, "[[a, b, c], [h, i, j]]");
            Add(3, 8, "[[a, b, c], [i, j, k]]");
            Add(3, 9, "[[a, b, c], [j, k, l]]");
            Add(3, 10, "[[a, b, c]]");
            Add(3, 11, "[[a, b, c]]");
            Add(3, 12, "[[a, b, c]]");

            Add(4, 1, "[[a, b, c, d], [b, c, d, e], [c, d, e, f], [d, e, f, g], [e, f, g, h], [f, g, h, i], [g, h, i, j], [h, i, j, k], [i, j, k, l]]");
            Add(4, 2, "[[a, b, c, d], [c, d, e, f], [e, f, g, h], [g, h, i, j], [i, j, k, l]]");
            Add(4, 3, "[[a, b, c, d], [d, e, f, g], [g, h, i, j]]");
            Add(4, 4, "[[a, b, c, d], [e, f, g, h], [i, j, k, l]]");
            Add(4, 5, "[[a, b, c, d], [f, g, h, i]]");
            Add(4, 6, "[[a, b, c, d], [g, h, i, j]]");
            Add(4, 7, "[[a, b, c, d], [h, i, j, k]]");
            Add(4, 8, "[[a, b, c, d], [i, j, k, l]]");
            Add(4, 9, "[[a, b, c, d]]");
            Add(4, 10, "[[a, b, c, d]]");
            Add(4, 11, "[[a, b, c, d]]");
            Add(4, 12, "[[a, b, c, d]]");

            Add(5, 1, "[[a, b, c, d, e], [b, c, d, e, f], [c, d, e, f, g], [d, e, f, g, h], [e, f, g, h, i], [f, g, h, i, j], [g, h, i, j, k], [h, i, j, k, l]]");
            Add(5, 2, "[[a, b, c, d, e], [c, d, e, f, g], [e, f, g, h, i], [g, h, i, j, k]]");
            Add(5, 3, "[[a, b, c, d, e], [d, e, f, g, h], [g, h, i, j, k]]");
            Add(5, 4, "[[a, b, c, d, e], [e, f, g, h, i]]");
            Add(5, 5, "[[a, b, c, d, e], [f, g, h, i, j]]");
            Add(5, 6, "[[a, b, c, d, e], [g, h, i, j, k]]");
            Add(5, 7, "[[a, b, c, d, e], [h, i, j, k, l]]");
            Add(5, 8, "[[a, b, c, d, e]]");
            Add(5, 9, "[[a, b, c, d, e]]");
            Add(5, 10, "[[a, b, c, d, e]]");
            Add(5, 11, "[[a, b, c, d, e]]");
            Add(5, 12, "[[a, b, c, d, e]]");

            Add(6, 1, "[[a, b, c, d, e, f], [b, c, d, e, f, g], [c, d, e, f, g, h], [d, e, f, g, h, i], [e, f, g, h, i, j], [f, g, h, i, j, k], [g, h, i, j, k, l]]");
            Add(6, 2, "[[a, b, c, d, e, f], [c, d, e, f, g, h], [e, f, g, h, i, j], [g, h, i, j, k, l]]");
            Add(6, 3, "[[a, b, c, d, e, f], [d, e, f, g, h, i], [g, h, i, j, k, l]]");
            Add(6, 4, "[[a, b, c, d, e, f], [e, f, g, h, i, j]]");
            Add(6, 5, "[[a, b, c, d, e, f], [f, g, h, i, j, k]]");
            Add(6, 6, "[[a, b, c, d, e, f], [g, h, i, j, k, l]]");
            Add(6, 7, "[[a, b, c, d, e, f]]");
            Add(6, 8, "[[a, b, c, d, e, f]]");
            Add(6, 9, "[[a, b, c, d, e, f]]");
            Add(6, 10, "[[a, b, c, d, e, f]]");
            Add(6, 11, "[[a, b, c, d, e, f]]");
            Add(6, 12, "[[a, b, c, d, e, f]]");

            Add(7, 1, "[[a, b, c, d, e, f, g], [b, c, d, e, f, g, h], [c, d, e, f, g, h, i], [d, e, f, g, h, i, j], [e, f, g, h, i, j, k], [f, g, h, i, j, k, l]]");
            Add(7, 2, "[[a, b, c, d, e, f, g], [c, d, e, f, g, h, i], [e, f, g, h, i, j, k]]");
            Add(7, 3, "[[a, b, c, d, e, f, g], [d, e, f, g, h, i, j]]");
            Add(7, 4, "[[a, b, c, d, e, f, g], [e, f, g, h, i, j, k]]");
            Add(7, 5, "[[a, b, c, d, e, f, g], [f, g, h, i, j, k, l]]");
            Add(7, 6, "[[a, b, c, d, e, f, g]]");
            Add(7, 7, "[[a, b, c, d, e, f, g]]");
            Add(7, 8, "[[a, b, c, d, e, f, g]]");
            Add(7, 9, "[[a, b, c, d, e, f, g]]");
            Add(7, 10, "[[a, b, c, d, e, f, g]]");
            Add(7, 11, "[[a, b, c, d, e, f, g]]");
            Add(7, 12, "[[a, b, c, d, e, f, g]]");

            Add(8, 1, "[[a, b, c, d, e, f, g, h], [b, c, d, e, f, g, h, i], [c, d, e, f, g, h, i, j], [d, e, f, g, h, i, j, k], [e, f, g, h, i, j, k, l]]");
            Add(8, 2, "[[a, b, c, d, e, f, g, h], [c, d, e, f, g, h, i, j], [e, f, g, h, i, j, k, l]]");
            Add(8, 3, "[[a, b, c, d, e, f, g, h], [d, e, f, g, h, i, j, k]]");
            Add(8, 4, "[[a, b, c, d, e, f, g, h], [e, f, g, h, i, j, k, l]]");
            Add(8, 5, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 6, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 7, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 8, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 9, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 10, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 11, "[[a, b, c, d, e, f, g, h]]");
            Add(8, 12, "[[a, b, c, d, e, f, g, h]]");

            Add(9, 1, "[[a, b, c, d, e, f, g, h, i], [b, c, d, e, f, g, h, i, j], [c, d, e, f, g, h, i, j, k], [d, e, f, g, h, i, j, k, l]]");
            Add(9, 2, "[[a, b, c, d, e, f, g, h, i], [c, d, e, f, g, h, i, j, k]]");
            Add(9, 3, "[[a, b, c, d, e, f, g, h, i], [d, e, f, g, h, i, j, k, l]]");
            Add(9, 4, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 5, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 6, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 7, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 8, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 9, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 10, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 11, "[[a, b, c, d, e, f, g, h, i]]");
            Add(9, 12, "[[a, b, c, d, e, f, g, h, i]]");

            Add(10, 1, "[[a, b, c, d, e, f, g, h, i, j], [b, c, d, e, f, g, h, i, j, k], [c, d, e, f, g, h, i, j, k, l]]");
            Add(10, 2, "[[a, b, c, d, e, f, g, h, i, j], [c, d, e, f, g, h, i, j, k, l]]");
            Add(10, 3, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 4, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 5, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 6, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 7, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 8, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 9, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 10, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 11, "[[a, b, c, d, e, f, g, h, i, j]]");
            Add(10, 12, "[[a, b, c, d, e, f, g, h, i, j]]");

            Add(11, 1, "[[a, b, c, d, e, f, g, h, i, j, k], [b, c, d, e, f, g, h, i, j, k, l]]");
            Add(11, 2, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 3, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 4, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 5, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 6, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 7, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 8, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 9, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 10, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 11, "[[a, b, c, d, e, f, g, h, i, j, k]]");
            Add(11, 12, "[[a, b, c, d, e, f, g, h, i, j, k]]");

            Add(12, 1, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 2, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 3, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 4, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 5, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 6, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 7, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 8, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 9, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 10, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 11, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(12, 12, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");

            Add(13, 1, "[[]]");
            Add(13, 2, "[[]]");
            Add(13, 3, "[[]]");
            Add(13, 4, "[[]]");
            Add(13, 5, "[[]]");
            Add(13, 6, "[[]]");
            Add(13, 7, "[[]]");
            Add(13, 8, "[[]]");
            Add(13, 9, "[[]]");
            Add(13, 10, "[[]]");
            Add(13, 11, "[[]]");
            Add(13, 12, "[[]]");
            Add(13, 13, "[[]]");
        }
    }

    private class PartialWindow : TheoryData<int, int, string>
    {
        public PartialWindow()
        {
            Add(1, 1, "[[a], [b], [c], [d], [e], [f], [g], [h], [i], [j], [k], [l]]");
            Add(1, 2, "[[a], [c], [e], [g], [i], [k]]");
            Add(1, 3, "[[a], [d], [g], [j]]");
            Add(1, 4, "[[a], [e], [i]]");
            Add(1, 5, "[[a], [f], [k]]");
            Add(1, 6, "[[a], [g]]");
            Add(1, 7, "[[a], [h]]");
            Add(1, 8, "[[a], [i]]");
            Add(1, 9, "[[a], [j]]");
            Add(1, 10, "[[a], [k]]");
            Add(1, 11, "[[a], [l]]");
            Add(1, 12, "[[a]]");

            Add(2, 1, "[[a, b], [b, c], [c, d], [d, e], [e, f], [f, g], [g, h], [h, i], [i, j], [j, k], [k, l], [l]]");
            Add(2, 2, "[[a, b], [c, d], [e, f], [g, h], [i, j], [k, l]]");
            Add(2, 3, "[[a, b], [d, e], [g, h], [j, k]]");
            Add(2, 4, "[[a, b], [e, f], [i, j]]");
            Add(2, 5, "[[a, b], [f, g], [k, l]]");
            Add(2, 6, "[[a, b], [g, h]]");
            Add(2, 7, "[[a, b], [h, i]]");
            Add(2, 8, "[[a, b], [i, j]]");
            Add(2, 9, "[[a, b], [j, k]]");
            Add(2, 10, "[[a, b], [k, l]]");
            Add(2, 11, "[[a, b], [l]]");
            Add(2, 12, "[[a, b]]");

            Add(3, 1, "[[a, b, c], [b, c, d], [c, d, e], [d, e, f], [e, f, g], [f, g, h], [g, h, i], [h, i, j], [i, j, k], [j, k, l], [k, l], [l]]");
            Add(3, 2, "[[a, b, c], [c, d, e], [e, f, g], [g, h, i], [i, j, k], [k, l]]");
            Add(3, 3, "[[a, b, c], [d, e, f], [g, h, i], [j, k, l]]");
            Add(3, 4, "[[a, b, c], [e, f, g], [i, j, k]]");
            Add(3, 5, "[[a, b, c], [f, g, h], [k, l]]");
            Add(3, 6, "[[a, b, c], [g, h, i]]");
            Add(3, 7, "[[a, b, c], [h, i, j]]");
            Add(3, 8, "[[a, b, c], [i, j, k]]");
            Add(3, 9, "[[a, b, c], [j, k, l]]");
            Add(3, 10, "[[a, b, c], [k, l]]");
            Add(3, 11, "[[a, b, c], [l]]");
            Add(3, 12, "[[a, b, c]]");

            Add(4, 1, "[[a, b, c, d], [b, c, d, e], [c, d, e, f], [d, e, f, g], [e, f, g, h], [f, g, h, i], [g, h, i, j], [h, i, j, k], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(4, 2, "[[a, b, c, d], [c, d, e, f], [e, f, g, h], [g, h, i, j], [i, j, k, l], [k, l]]");
            Add(4, 3, "[[a, b, c, d], [d, e, f, g], [g, h, i, j], [j, k, l]]");
            Add(4, 4, "[[a, b, c, d], [e, f, g, h], [i, j, k, l]]");
            Add(4, 5, "[[a, b, c, d], [f, g, h, i], [k, l]]");
            Add(4, 6, "[[a, b, c, d], [g, h, i, j]]");
            Add(4, 7, "[[a, b, c, d], [h, i, j, k]]");
            Add(4, 8, "[[a, b, c, d], [i, j, k, l]]");
            Add(4, 9, "[[a, b, c, d], [j, k, l]]");
            Add(4, 10, "[[a, b, c, d], [k, l]]");
            Add(4, 11, "[[a, b, c, d], [l]]");
            Add(4, 12, "[[a, b, c, d]]");

            Add(5, 1, "[[a, b, c, d, e], [b, c, d, e, f], [c, d, e, f, g], [d, e, f, g, h], [e, f, g, h, i], [f, g, h, i, j], [g, h, i, j, k], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(5, 2, "[[a, b, c, d, e], [c, d, e, f, g], [e, f, g, h, i], [g, h, i, j, k], [i, j, k, l], [k, l]]");
            Add(5, 3, "[[a, b, c, d, e], [d, e, f, g, h], [g, h, i, j, k], [j, k, l]]");
            Add(5, 4, "[[a, b, c, d, e], [e, f, g, h, i], [i, j, k, l]]");
            Add(5, 5, "[[a, b, c, d, e], [f, g, h, i, j], [k, l]]");
            Add(5, 6, "[[a, b, c, d, e], [g, h, i, j, k]]");
            Add(5, 7, "[[a, b, c, d, e], [h, i, j, k, l]]");
            Add(5, 8, "[[a, b, c, d, e], [i, j, k, l]]");
            Add(5, 9, "[[a, b, c, d, e], [j, k, l]]");
            Add(5, 10, "[[a, b, c, d, e], [k, l]]");
            Add(5, 11, "[[a, b, c, d, e], [l]]");
            Add(5, 12, "[[a, b, c, d, e]]");

            Add(6, 1, "[[a, b, c, d, e, f], [b, c, d, e, f, g], [c, d, e, f, g, h], [d, e, f, g, h, i], [e, f, g, h, i, j], [f, g, h, i, j, k], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(6, 2, "[[a, b, c, d, e, f], [c, d, e, f, g, h], [e, f, g, h, i, j], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(6, 3, "[[a, b, c, d, e, f], [d, e, f, g, h, i], [g, h, i, j, k, l], [j, k, l]]");
            Add(6, 4, "[[a, b, c, d, e, f], [e, f, g, h, i, j], [i, j, k, l]]");
            Add(6, 5, "[[a, b, c, d, e, f], [f, g, h, i, j, k], [k, l]]");
            Add(6, 6, "[[a, b, c, d, e, f], [g, h, i, j, k, l]]");
            Add(6, 7, "[[a, b, c, d, e, f], [h, i, j, k, l]]");
            Add(6, 8, "[[a, b, c, d, e, f], [i, j, k, l]]");
            Add(6, 9, "[[a, b, c, d, e, f], [j, k, l]]");
            Add(6, 10, "[[a, b, c, d, e, f], [k, l]]");
            Add(6, 11, "[[a, b, c, d, e, f], [l]]");
            Add(6, 12, "[[a, b, c, d, e, f]]");

            Add(7, 1, "[[a, b, c, d, e, f, g], [b, c, d, e, f, g, h], [c, d, e, f, g, h, i], [d, e, f, g, h, i, j], [e, f, g, h, i, j, k], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(7, 2, "[[a, b, c, d, e, f, g], [c, d, e, f, g, h, i], [e, f, g, h, i, j, k], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(7, 3, "[[a, b, c, d, e, f, g], [d, e, f, g, h, i, j], [g, h, i, j, k, l], [j, k, l]]");
            Add(7, 4, "[[a, b, c, d, e, f, g], [e, f, g, h, i, j, k], [i, j, k, l]]");
            Add(7, 5, "[[a, b, c, d, e, f, g], [f, g, h, i, j, k, l], [k, l]]");
            Add(7, 6, "[[a, b, c, d, e, f, g], [g, h, i, j, k, l]]");
            Add(7, 7, "[[a, b, c, d, e, f, g], [h, i, j, k, l]]");
            Add(7, 8, "[[a, b, c, d, e, f, g], [i, j, k, l]]");
            Add(7, 9, "[[a, b, c, d, e, f, g], [j, k, l]]");
            Add(7, 10, "[[a, b, c, d, e, f, g], [k, l]]");
            Add(7, 11, "[[a, b, c, d, e, f, g], [l]]");
            Add(7, 12, "[[a, b, c, d, e, f, g]]");

            Add(8, 1, "[[a, b, c, d, e, f, g, h], [b, c, d, e, f, g, h, i], [c, d, e, f, g, h, i, j], [d, e, f, g, h, i, j, k], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(8, 2, "[[a, b, c, d, e, f, g, h], [c, d, e, f, g, h, i, j], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(8, 3, "[[a, b, c, d, e, f, g, h], [d, e, f, g, h, i, j, k], [g, h, i, j, k, l], [j, k, l]]");
            Add(8, 4, "[[a, b, c, d, e, f, g, h], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(8, 5, "[[a, b, c, d, e, f, g, h], [f, g, h, i, j, k, l], [k, l]]");
            Add(8, 6, "[[a, b, c, d, e, f, g, h], [g, h, i, j, k, l]]");
            Add(8, 7, "[[a, b, c, d, e, f, g, h], [h, i, j, k, l]]");
            Add(8, 8, "[[a, b, c, d, e, f, g, h], [i, j, k, l]]");
            Add(8, 9, "[[a, b, c, d, e, f, g, h], [j, k, l]]");
            Add(8, 10, "[[a, b, c, d, e, f, g, h], [k, l]]");
            Add(8, 11, "[[a, b, c, d, e, f, g, h], [l]]");
            Add(8, 12, "[[a, b, c, d, e, f, g, h]]");

            Add(9, 1, "[[a, b, c, d, e, f, g, h, i], [b, c, d, e, f, g, h, i, j], [c, d, e, f, g, h, i, j, k], [d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(9, 2, "[[a, b, c, d, e, f, g, h, i], [c, d, e, f, g, h, i, j, k], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(9, 3, "[[a, b, c, d, e, f, g, h, i], [d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [j, k, l]]");
            Add(9, 4, "[[a, b, c, d, e, f, g, h, i], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(9, 5, "[[a, b, c, d, e, f, g, h, i], [f, g, h, i, j, k, l], [k, l]]");
            Add(9, 6, "[[a, b, c, d, e, f, g, h, i], [g, h, i, j, k, l]]");
            Add(9, 7, "[[a, b, c, d, e, f, g, h, i], [h, i, j, k, l]]");
            Add(9, 8, "[[a, b, c, d, e, f, g, h, i], [i, j, k, l]]");
            Add(9, 9, "[[a, b, c, d, e, f, g, h, i], [j, k, l]]");
            Add(9, 10, "[[a, b, c, d, e, f, g, h, i], [k, l]]");
            Add(9, 11, "[[a, b, c, d, e, f, g, h, i], [l]]");
            Add(9, 12, "[[a, b, c, d, e, f, g, h, i]]");

            Add(10, 1, "[[a, b, c, d, e, f, g, h, i, j], [b, c, d, e, f, g, h, i, j, k], [c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(10, 2, "[[a, b, c, d, e, f, g, h, i, j], [c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(10, 3, "[[a, b, c, d, e, f, g, h, i, j], [d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [j, k, l]]");
            Add(10, 4, "[[a, b, c, d, e, f, g, h, i, j], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(10, 5, "[[a, b, c, d, e, f, g, h, i, j], [f, g, h, i, j, k, l], [k, l]]");
            Add(10, 6, "[[a, b, c, d, e, f, g, h, i, j], [g, h, i, j, k, l]]");
            Add(10, 7, "[[a, b, c, d, e, f, g, h, i, j], [h, i, j, k, l]]");
            Add(10, 8, "[[a, b, c, d, e, f, g, h, i, j], [i, j, k, l]]");
            Add(10, 9, "[[a, b, c, d, e, f, g, h, i, j], [j, k, l]]");
            Add(10, 10, "[[a, b, c, d, e, f, g, h, i, j], [k, l]]");
            Add(10, 11, "[[a, b, c, d, e, f, g, h, i, j], [l]]");
            Add(10, 12, "[[a, b, c, d, e, f, g, h, i, j]]");

            Add(11, 1, "[[a, b, c, d, e, f, g, h, i, j, k], [b, c, d, e, f, g, h, i, j, k, l], [c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(11, 2, "[[a, b, c, d, e, f, g, h, i, j, k], [c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(11, 3, "[[a, b, c, d, e, f, g, h, i, j, k], [d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [j, k, l]]");
            Add(11, 4, "[[a, b, c, d, e, f, g, h, i, j, k], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(11, 5, "[[a, b, c, d, e, f, g, h, i, j, k], [f, g, h, i, j, k, l], [k, l]]");
            Add(11, 6, "[[a, b, c, d, e, f, g, h, i, j, k], [g, h, i, j, k, l]]");
            Add(11, 7, "[[a, b, c, d, e, f, g, h, i, j, k], [h, i, j, k, l]]");
            Add(11, 8, "[[a, b, c, d, e, f, g, h, i, j, k], [i, j, k, l]]");
            Add(11, 9, "[[a, b, c, d, e, f, g, h, i, j, k], [j, k, l]]");
            Add(11, 10, "[[a, b, c, d, e, f, g, h, i, j, k], [k, l]]");
            Add(11, 11, "[[a, b, c, d, e, f, g, h, i, j, k], [l]]");
            Add(11, 12, "[[a, b, c, d, e, f, g, h, i, j, k]]");

            Add(12, 1, "[[a, b, c, d, e, f, g, h, i, j, k, l], [b, c, d, e, f, g, h, i, j, k, l], [c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(12, 2, "[[a, b, c, d, e, f, g, h, i, j, k, l], [c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(12, 3, "[[a, b, c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [j, k, l]]");
            Add(12, 4, "[[a, b, c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(12, 5, "[[a, b, c, d, e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [k, l]]");
            Add(12, 6, "[[a, b, c, d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l]]");
            Add(12, 7, "[[a, b, c, d, e, f, g, h, i, j, k, l], [h, i, j, k, l]]");
            Add(12, 8, "[[a, b, c, d, e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(12, 9, "[[a, b, c, d, e, f, g, h, i, j, k, l], [j, k, l]]");
            Add(12, 10, "[[a, b, c, d, e, f, g, h, i, j, k, l], [k, l]]");
            Add(12, 11, "[[a, b, c, d, e, f, g, h, i, j, k, l], [l]]");
            Add(12, 12, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");

            Add(13, 1, "[[a, b, c, d, e, f, g, h, i, j, k, l], [b, c, d, e, f, g, h, i, j, k, l], [c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [g, h, i, j, k, l], [h, i, j, k, l], [i, j, k, l], [j, k, l], [k, l], [l]]");
            Add(13, 2, "[[a, b, c, d, e, f, g, h, i, j, k, l], [c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [i, j, k, l], [k, l]]");
            Add(13, 3, "[[a, b, c, d, e, f, g, h, i, j, k, l], [d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l], [j, k, l]]");
            Add(13, 4, "[[a, b, c, d, e, f, g, h, i, j, k, l], [e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(13, 5, "[[a, b, c, d, e, f, g, h, i, j, k, l], [f, g, h, i, j, k, l], [k, l]]");
            Add(13, 6, "[[a, b, c, d, e, f, g, h, i, j, k, l], [g, h, i, j, k, l]]");
            Add(13, 7, "[[a, b, c, d, e, f, g, h, i, j, k, l], [h, i, j, k, l]]");
            Add(13, 8, "[[a, b, c, d, e, f, g, h, i, j, k, l], [i, j, k, l]]");
            Add(13, 9, "[[a, b, c, d, e, f, g, h, i, j, k, l], [j, k, l]]");
            Add(13, 10, "[[a, b, c, d, e, f, g, h, i, j, k, l], [k, l]]");
            Add(13, 11, "[[a, b, c, d, e, f, g, h, i, j, k, l], [l]]");
            Add(13, 12, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
            Add(13, 13, "[[a, b, c, d, e, f, g, h, i, j, k, l]]");
        }
    }
}
