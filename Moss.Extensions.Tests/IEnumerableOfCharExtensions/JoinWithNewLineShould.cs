namespace Moss.Extensions.Tests.IEnumerableOfCharExtensions;

public class JoinWithNewLineShould
{
    [Fact]
    public void JoinStringsTogetherUsingNewline()
    {
        var tokens = new List<char> { 's', 'p', 'a', 'm' };

        var result = tokens.JoinWithNewLine();

        result.ShouldBe($"s{Environment.NewLine}p{Environment.NewLine}a{Environment.NewLine}m");
    }
}
