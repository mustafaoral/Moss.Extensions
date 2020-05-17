using System;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.DateTimeExtensions
{
    public class ToIso8601DateStringShould
    {
        [Fact]
        public void ConvertValueToIso8601Date()
        {
            var result = new DateTime(2015, 10, 21).ToIso8601DateString();

            result.ShouldBe("2015-10-21");
        }
    }
}
