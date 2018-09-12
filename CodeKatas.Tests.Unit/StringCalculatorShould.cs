using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class StringCalculatorShould
    {
        private readonly StringCalculator _stringCalculator = new StringCalculator();

        [Test]
        public void ReturnZero_WhenAdding_AndGivenAnEmptyString()
        {
            var actualResult = _stringCalculator.Add(string.Empty);

            actualResult.Should().Be(0);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("4", 4)]
        public void ReturnSingleNumber_WhenAdding_GivenSingleStringNumber(string stringInputNumber, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(stringInputNumber);

            actualResult.Should().Be(expectedResult);
        }

        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("4,5", 9)]
        public void ReturnSummedNumber_WhenAdding_GivenTwoStringNumberSeparatedByAComma(string stringInputNumber, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(stringInputNumber);

            actualResult.Should().Be(expectedResult);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("2,2,3", 7)]
        [TestCase("100,2,3", 105)]
        public void ReturnSummedNumber_WhenAdding_GivenUnknownNumberOfSeparatedStringNumbers(string stringInputNumber, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(stringInputNumber);

            actualResult.Should().Be(expectedResult);
        }

        [TestCase("1\n2,3", 6)]
        public void ReturnSummedNumber_WhenAdding_GivenTwoStringNumbersSeparatedByTwoDifferentDelimiters(string stringInputNumber, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(stringInputNumber);

            actualResult.Should().Be(expectedResult);
        }
    }
}
