using System;
using System.IO;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.FIleInfoExtensions
{
    public class HasExtensionWithStringComparisonShould
    {
        [Theory]
        [InlineData("foo.bar", ".bar", StringComparison.Ordinal, true)]
        [InlineData("foo.bar", ".Bar", StringComparison.Ordinal, false)]
        [InlineData("foo.bar", ".baz", StringComparison.Ordinal, false)]
        [InlineData("foo.bar", ".bar", StringComparison.OrdinalIgnoreCase, true)]
        [InlineData("foo.bar", ".Bar", StringComparison.OrdinalIgnoreCase, true)]
        [InlineData("foo.bar", ".baz", StringComparison.OrdinalIgnoreCase, false)]
        [InlineData("foo.bar", ".bar", StringComparison.InvariantCulture, true)]
        [InlineData("foo.bar", ".Bar", StringComparison.InvariantCulture, false)]
        [InlineData("foo.bar", ".baz", StringComparison.InvariantCulture, false)]
        [InlineData("foo.bar", ".bar", StringComparison.InvariantCultureIgnoreCase, true)]
        [InlineData("foo.bar", ".Bar", StringComparison.InvariantCultureIgnoreCase, true)]
        [InlineData("foo.bar", ".baz", StringComparison.InvariantCultureIgnoreCase, false)]
        public void ReturnExpectedResult(string fileName, string extension, StringComparison stringComparison, bool expectedResult)
        {
            var fileInfo = new FileInfo(fileName);

            fileInfo.HasExtension(extension, stringComparison).ShouldBe(expectedResult);
        }
    }
}
