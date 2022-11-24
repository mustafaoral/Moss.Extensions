using System.IO;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.FIleInfoExtensions
{
    public class HasAnyExtensionShould
    {
        [Theory]
        [InlineData("foo.bar", new[] { ".bar" }, true)]
        [InlineData("foo.bar", new[] { ".Bar" }, false)]
        [InlineData("foo.bar", new[] { ".baz" }, false)]
        public void ReturnExpectedResult(string fileName, string[] extensions, bool expectedResult)
        {
            var fileInfo = new FileInfo(fileName);

            fileInfo.HasAnyExtension(extensions).ShouldBe(expectedResult);
        }
    }
}
