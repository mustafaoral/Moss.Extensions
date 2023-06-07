namespace Moss.Extensions.Tests.FIleInfoExtensions;

public class HasExtensionShould
{
    [Theory]
    [InlineData("foo.bar", ".bar", true)]
    [InlineData("foo.bar", ".Bar", false)]
    [InlineData("foo.bar", ".baz", false)]
    public void ReturnExpectedResult(string fileName, string extension, bool expectedResult)
    {
        var fileInfo = new FileInfo(fileName);

        fileInfo.HasExtension(extension).ShouldBe(expectedResult);
    }
}
