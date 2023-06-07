namespace Moss.Extensions.Tests.IEnumerableOfCharExtensions;

public class JoinWithCommaShould
{
    [Fact]
    public void JoinStringsTogetherUsingSeparator()
    {
        var tokens = new List<char> { 's', 'p', 'a', 'm' };

        var result = tokens.JoinWithComma();

        result.ShouldBe("s,p,a,m");
    }
}
