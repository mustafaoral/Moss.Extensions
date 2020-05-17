using System.Globalization;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.DecimalExtensions
{
    public class ToCurrencyStringShould
    {
        [Fact]
        public void ConvertValueToCurrencyStringUsingCultureInfo()
        {
            var result = 42_000_000m.ToCurrencyString(new CultureInfo("en-us"));

            result.ShouldBe("$42,000,000.00");
        }
    }
}
