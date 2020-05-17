using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.StringExtensions
{
    public class SplitToTokensInLengthShould
    {
        [Fact]
        public void ThrowExceptionIfStringInstanceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((string)null).SplitToTokensInLength(3).ToList());

            exception.Message.ShouldBe("String instance is null. (Parameter 'value')");
        }

        [Fact]
        public void ThrowExceptionIfTokenLengthIsLessThanOne()
        {
            var exception = Assert.Throws<ArgumentException>(() => "spam and eggs".SplitToTokensInLength(0).ToList());

            exception.Message.ShouldBe("Token length must be greater than or equal to 1.");
        }

        [Fact]
        public void SplitInputIntoTokensOfGivenLength()
        {
            var result = "123456789".SplitToTokensInLength(3).ToList();

            result.Count.ShouldBe(3);
            result[0].ShouldBe("123");
            result[1].ShouldBe("456");
            result[2].ShouldBe("789");
        }

        [Fact]
        public void SplitInputIntoTokensOfGivenLengthAndPadLastItem()
        {
            var result = "123456789".SplitToTokensInLength(5).ToList();

            result.Count.ShouldBe(2);
            result[0].ShouldBe("12345");
            result[1].ShouldBe("6789 ");
        }

        [Fact]
        public void ReturnEmptyEnumerableIfInputIsEmptyString()
        {
            var result = string.Empty.SplitToTokensInLength(5).ToList();

            result.ShouldBeEmpty();
        }
    }
}
