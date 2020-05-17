using System.Text.RegularExpressions;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.RegexExtensions
{
    public class TryMatchShould
    {
        [Fact]
        public void ReturnTrueWhenMatchSucceeds()
        {
            var regex = new Regex("spam");

            var result = regex.TryMatch("spam and eggs", out var match);

            result.ShouldBeTrue();
            match.Success.ShouldBeTrue();
            match.Value.ShouldBe("spam");
        }

        [Fact]
        public void ReturnFalseWhenMatchFails()
        {
            var regex = new Regex("foo");

            var result = regex.TryMatch("bar", out var match);

            result.ShouldBeFalse();
            match.ShouldBeNull();
        }
    }
}
