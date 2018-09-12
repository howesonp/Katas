using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class CalculationExtensionShould
    {
        [TestCase(15, 5, true)]
        [TestCase(15, 6, false)]
        public void ReturnTrueOrFalse_WhenCheckingDivisible_GivenInputs(int input, int divisibleBy, bool expectedResult)
        {
            var actualResult = input.IsDivisibleBy(divisibleBy);

            actualResult.Should().Be(expectedResult);
        }
    }
}
