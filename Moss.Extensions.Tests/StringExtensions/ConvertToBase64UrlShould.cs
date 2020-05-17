using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.StringExtensions
{
    public class ConvertToBase64UrlShould
    {
        [Fact]
        public void ConvertValueToBase64()
        {
            var result = "<<???>>".ConvertToBase64Url();

            result.ShouldBe("PDw_Pz8-Pg");
        }
    }
}
