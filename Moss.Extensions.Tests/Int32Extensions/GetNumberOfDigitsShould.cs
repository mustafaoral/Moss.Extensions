namespace Moss.Extensions.Tests.Int32Extensions;

public class GetNumberOfDigitsShould
{
    [Theory]
    [InlineData(1E0, 1)]
    [InlineData(1E1, 2)]
    [InlineData(1E2, 3)]
    [InlineData(1E3, 4)]
    [InlineData(1E4, 5)]
    [InlineData(1E5, 6)]
    [InlineData(1E6, 7)]
    [InlineData(1E7, 8)]
    [InlineData(1E8, 9)]
    [InlineData(1E9, 10)]
    [InlineData(-1E0, 1)]
    [InlineData(-1E1, 2)]
    [InlineData(-1E2, 3)]
    [InlineData(-1E3, 4)]
    [InlineData(-1E4, 5)]
    [InlineData(-1E5, 6)]
    [InlineData(-1E6, 7)]
    [InlineData(-1E7, 8)]
    [InlineData(-1E8, 9)]
    [InlineData(-1E9, 10)]
    [InlineData(int.MaxValue, 10)]
    [InlineData(int.MinValue, 10)]
    public void ReturnTheNumberOfDigits(int value, int expectedResult)
    {
        value.GetNumberOfDigits().ShouldBe(expectedResult);
    }
}
