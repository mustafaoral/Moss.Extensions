namespace Moss.Extensions.Tests.ByteExtensions;

public class ToHexStringShould
{
    [Fact]
    public void ConvertValueIntoHexString()
    {
        byte value = 0x_42;

        value.ToHexString().ShouldBe("42");
    }
}
