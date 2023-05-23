using FluentAssertions;
using Xunit;

namespace GNB.SalesEnquiry.Shared.Test
{
    public class doubleExtendersTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1.2, 1.2)]
        [InlineData(-2.3, -2.3)]
        [InlineData(1.5, 1.5)]
        [InlineData(-1.5, -1.5)]
        [InlineData(3.345, 3.34)]
        [InlineData(3.355, 3.36)]

        public void ToDecimalBankersRounding_rounds_correctly(double value, decimal expectedValue)
        {
            var result = value.ToDecimalBankersRouding();
            result.Should().Be(expectedValue);
        }
    }
}
