namespace Moss.Extensions.Tests.IEnumerableOfStringExtensions;

public class JoinWithShould
{
    [Fact]
    public void JoinStringsTogetherUsingSeparator()
    {
        var tokens = new List<string> { "spam", "and", "eggs" };

        var result = tokens.JoinWith("|");

        result.ShouldBe("spam|and|eggs");
    }
}
