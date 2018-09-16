using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CodeKatas.Tests.Unit
{
    [TestFixture]
    public class BowlingGameShould
    {
        private readonly BowlingGame _bowlingGame = new BowlingGame();

        [Test]
        public void ReturnZero_WhenScoring_AndNoPinsAreHit()
        { 
            var game = "--|--|--|--|--|--|--|--|--|--||--";

            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(0);
        }

        [TestCase("1-|--|--|--|--|--|--|--|--|--||--", 1)]
        [TestCase("2-|--|--|--|--|--|--|--|--|--||--", 2)]
        [TestCase("3-|--|--|--|--|--|--|--|--|--||--", 3)]
        public void ReturnSingleNumber_WhenScoring_AndFirstThrowPinsAreHit(string game, int expectedScore)
        {

            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("11|--|--|--|--|--|--|--|--|--||--", 2)]
        [TestCase("22|--|--|--|--|--|--|--|--|--||--", 4)]
        [TestCase("27|--|--|--|--|--|--|--|--|--||--", 9)]
        public void ReturnSummedNumber_WhenScoring_AndFirstTwoThrowPinsAreHit(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("-1|--|--|--|--|--|--|--|--|--||--", 1)]
        [TestCase("-2|--|--|--|--|--|--|--|--|--||--", 2)]
        public void ReturnSingleNumber_WhenScoring_AndSecondThrowPinsAreHit(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|--|--|--|--|--|--|--|--|--||--", 10)]
        [TestCase("/-|--|--|--|--|--|--|--|--|--||--", 10)]
        [TestCase("-X|--|--|--|--|--|--|--|--|--||--", 10)]
        [TestCase("-/|--|--|--|--|--|--|--|--|--||--", 10)]
        public void ReturnTen_WhenScoring_AndFirstOrSecondThrowScoresAStrikeOrSpare(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("1-|1-|--|--|--|--|--|--|--|--||--", 2)]
        [TestCase("18|1-|--|-4|--|41|--|52|--|--||--", 26)]
        public void ReturnSummedScore_WhenScoring_AndMultipleHitsInMultipleFrames(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|--|--|--|--|--|--|--|--|--||--", 10)]
        [TestCase("X-|--|X-|--|--|--|X-|--|--|--||--", 30)]
        public void ReturnScore_WhenScoring_AndStrikeIsHitOnFirstThrow(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|5-|--|--|--|--|--|--|--|--||--", 20)]
        public void ReturnTotalScoreOfTwenty_WhenScoring_AndStrikeInFirstFrameThen5(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }
    }
}
