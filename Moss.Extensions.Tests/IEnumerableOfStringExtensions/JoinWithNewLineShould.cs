namespace Moss.Extensions.Tests.IEnumerableOfStringExtensions;

public class JoinWithNewLineShould
{
    [Fact]
    public void JoinStringsTogetherUsingNewline()
    {
        var tokens = new List<string> { "spam", "and", "eggs" };

        var result = tokens.JoinWithNewLine();

        result.ShouldBe($"spam{Environment.NewLine}and{Environment.NewLine}eggs");
    }
}
