using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.ByteExtensions
{
    public class ToBitStringShould
    {
        [Fact]
        public void ConvertValueToBitString()
        {
            byte value = 0x_42;

            value.ToBitString().ShouldBe("01000010");
        }
    }
}
