namespace Moss.Extensions.Tests.StringExtensions;

public class ConvertToBase64Should
{
    [Fact]
    public void ConvertValueToBase64()
    {
        var result = "spam and eggs".ConvertToBase64();

        result.ShouldBe("c3BhbSBhbmQgZWdncw==");
    }
}
