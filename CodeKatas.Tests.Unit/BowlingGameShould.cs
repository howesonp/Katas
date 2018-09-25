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
        [TestCase("X-|--|23|--|--|--|2-|--|--|--||--", 17)]
        public void ReturnScore_WhenScoring_AndStrikeIsHitOnFirstThrow(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|5-|--|--|--|--|--|--|--|--||--", 20)]
        [TestCase("X-|X-|X-|11|--|--|--|--|--|--||--", 65)]
        public void ReturnTotalScore_WhenScoring_AndStrikeInFirstFrameThenScore(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("-/|5-|--|--|--|--|--|--|--|--||--", 20)]
        [TestCase("-/|51|--|--|--|--|--|--|--|--||--", 21)]
        [TestCase("2/|5-|--|--|--|--|--|--|--|--||--", 20)]
        [TestCase("2/|-4|5/|X-|--|--|--|--|--|--||--", 44)]
        public void ReturnTotalScore_WhenScoring_AndSpareInFirstFrameThenScore(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|5-|--|--|--|--|--|--|--|X-||11", 32)]
        public void ReturnTotalScore_WhenScoring_AndStrikeInFinalFrame(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|5-|--|--|--|--|--|--|--|2/||11", 31)]
        public void ReturnTotalScore_WhenScoring_AndSpareInFinalFrame(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }

        [TestCase("X-|5-|--|--|--|--|--|--|--|2-||11", 22)]
        [TestCase("X-|5-|--|--|--|--|--|--|--|27||18", 29)]
        [TestCase("X-|X-|X-|X-|X-|X-|X-|X-|X-|X-||XX", 300)]
        [TestCase("--|--|--|--|--|--|--|--|--|X-||XX", 30)]
        [TestCase("--|--|--|--|--|--|--|--|--|2/||8X", 18)]
        public void ReturnTotalScoreExcludingBonusRound_WhenScoring_AndNoStrikeOrSpareInFinalFrame(string game, int expectedScore)
        {
            var actualScore = _bowlingGame.Score(game);

            actualScore.Should().Be(expectedScore);
        }
    }
}
