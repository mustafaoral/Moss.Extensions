using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.ByteExtensions
{
    public class ToBitCharsShould
    {
        [Fact]
        public void ConvertValueToBitChars()
        {
            byte value = 0x_42;

            value.ToBitChars().ShouldBe("01000010".ToCharArray());
        }
    }
}
