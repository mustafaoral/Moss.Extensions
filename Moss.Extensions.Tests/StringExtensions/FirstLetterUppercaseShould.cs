namespace Moss.Extensions.Tests.StringExtensions;

public class FirstLetterUppercaseShould
{
    [Fact]
    public void ThrowExceptionIfStringInstanceIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => ((string)null).FirstLetterUppercase());

        exception.Message.ShouldBe("String instance is null. (Parameter 'value')");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ReturnSameValueIfEmptyStringOrWhiteSpace(string value)
    {
        var result = value.FirstLetterUppercase();

        result.ShouldBe(value);
    }

    [Theory]
    [InlineData("abc", "Abc")]
    public void ReturnFirstLetterUppercase(string value, string expectedValue)
    {
        var result = value.FirstLetterUppercase();

        result.ShouldBe(expectedValue);
    }
}
