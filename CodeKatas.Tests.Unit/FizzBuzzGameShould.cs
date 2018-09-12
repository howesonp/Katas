using System;
using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class FizzBuzzGameShould
    {
        [TestCase(1, "1")]
        [TestCase(98, "98")]
        [TestCase(3, "FIZZ")]
        [TestCase(5, "BUZZ")]
        [TestCase(15, "FIZZBUZZ")]
        [TestCase(30, "FIZZBUZZ")]
        public void ReturnCorrectNumberOrWordGivenAnInput(int input, string expectedResult)
        {
            var game = new FizzBuzzGame();

            var result = game.CalculateFizzBuzz(input);

            result.Should().Be(expectedResult);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(101)]
        public void ThrowOutOfRangeExceptionWhenNotBetweenOneAndOneHundred(int input)
        {
            var game = new FizzBuzzGame();

            Assert.Throws<ArgumentOutOfRangeException>(() => game.CalculateFizzBuzz(input));
        }
    }
}
