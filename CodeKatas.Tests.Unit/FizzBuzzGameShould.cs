using System;
using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class FizzBuzzGameShould
    {
        //[Test]
        //public void PrintNumbers_From1To100()
        //{
        //    var game = new FizzBuzzGame();

        //    var result = game.Play();

        //    result.Count().Should().Be(99);
        //}

        //[Test]
        //public void ReturnFizz_WhenNumberIsDivisibleByThree()
        //{
        //    var game = new FizzBuzzGame();

        //    var result = game.Play();

        //    for (var i = 1; i < 100; i+=2)
        //    {
        //        result.Should().Equal("FIZZ");
        //    }
        //}

        //[Test]
        //public void ReturnBuzz_WhenNumberIsDivisibleByFive()
        //{
        //    var game = new FizzBuzzGame();

        //    var result = game.Play();

        //    for (var i = 1; i < 100; i += 4)
        //    {
        //        result.Should().Equal("BUZZ");
        //    }
        //}

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
