namespace Moss.Extensions.Tests.IEnumerableOfCharExtensions;

public class JoinWithSpaceShould
{
    [Fact]
    public void JoinStringsTogetherUsingSpace()
    {
        var tokens = new List<char> { 's', 'p', 'a', 'm' };

        var result = tokens.JoinWithSpace();

        result.ShouldBe("s p a m");
    }
}
