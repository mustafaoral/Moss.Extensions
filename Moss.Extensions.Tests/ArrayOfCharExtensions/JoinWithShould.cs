using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.ArrayOfCharExtensions
{
    public class JoinWithShould
    {
        [Fact]
        public void JoinCharsUsingSeparator()
        {
            var result = new char[] { 'a', 'b' }.JoinWith("|");

            result.ShouldBe("a|b");
        }
    }
}
