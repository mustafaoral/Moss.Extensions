using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfCharExtensions
{
    public class JoinWithShould
    {
        [Fact]
        public void JoinStringsTogetherUsingSeparator()
        {
            var tokens = new List<char> { 's', 'p', 'a', 'm' };

            var result = tokens.JoinWith("|");

            result.ShouldBe("s|p|a|m");
        }
    }
}
