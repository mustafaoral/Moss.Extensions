using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfStringExtensions
{
    public class JoinWithSpaceShould
    {
        [Fact]
        public void JoinStringsTogetherUsingSpace()
        {
            var tokens = new List<string> { "spam", "and", "eggs" };

            var result = tokens.JoinWithSpace();

            result.ShouldBe("spam and eggs");
        }
    }
}
