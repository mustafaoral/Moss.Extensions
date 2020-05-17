using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfByteExtensions
{
    public class ToHexStringShould
    {
        [Fact]
        public void ConvertBytesToHexString()
        {
            var result = new byte[] { 0x_12, 0x_23 }.ToHexString();

            result.ShouldBe("1223");
        }

        [Fact]
        public void ConvertBytesToHexStringUsingSeparator()
        {
            var result = new byte[] { 0x_12, 0x_23 }.ToHexString("|");

            result.ShouldBe("12|23");
        }
    }
}
