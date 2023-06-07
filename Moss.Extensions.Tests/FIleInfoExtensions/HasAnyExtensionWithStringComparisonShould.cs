namespace Moss.Extensions.Tests.FIleInfoExtensions;

public class HasAnyExtensionWithStringComparisonShould
{
    [Theory]
    [InlineData("foo.bar", new[] { ".bar" }, StringComparison.Ordinal, true)]
    [InlineData("foo.bar", new[] { ".Bar" }, StringComparison.Ordinal, false)]
    [InlineData("foo.bar", new[] { ".baz" }, StringComparison.Ordinal, false)]
    [InlineData("foo.bar", new[] { ".bar" }, StringComparison.OrdinalIgnoreCase, true)]
    [InlineData("foo.bar", new[] { ".Bar" }, StringComparison.OrdinalIgnoreCase, true)]
    [InlineData("foo.bar", new[] { ".baz" }, StringComparison.OrdinalIgnoreCase, false)]
    [InlineData("foo.bar", new[] { ".bar" }, StringComparison.InvariantCulture, true)]
    [InlineData("foo.bar", new[] { ".Bar" }, StringComparison.InvariantCulture, false)]
    [InlineData("foo.bar", new[] { ".baz" }, StringComparison.InvariantCulture, false)]
    [InlineData("foo.bar", new[] { ".bar" }, StringComparison.InvariantCultureIgnoreCase, true)]
    [InlineData("foo.bar", new[] { ".Bar" }, StringComparison.InvariantCultureIgnoreCase, true)]
    [InlineData("foo.bar", new[] { ".baz" }, StringComparison.InvariantCultureIgnoreCase, false)]
    public void ReturnExpectedResult(string fileName, string[] extensions, StringComparison stringComparison, bool expectedResult)
    {
        var fileInfo = new FileInfo(fileName);

        fileInfo.HasAnyExtension(extensions, stringComparison).ShouldBe(expectedResult);
    }
}
