using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfStringExtensions
{
    public class JoinWithCommaShould
    {
        [Fact]
        public void JoinStringsTogetherUsingSeparator()
        {
            var tokens = new List<string> { "spam", "and", "eggs" };

            var result = tokens.JoinWithComma();

            result.ShouldBe("spam,and,eggs");
        }
    }
}
