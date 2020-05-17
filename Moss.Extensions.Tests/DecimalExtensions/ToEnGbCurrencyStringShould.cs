using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.DecimalExtensions
{
    public class ToEnGbCurrencyStringShould
    {
        [Fact]
        public void ConvertValueToBritishPoundSterling()
        {
            var result = 42_000_000m.ToEnGbCurrencyString();

            result.ShouldBe("£42,000,000.00");
        }
    }
}
