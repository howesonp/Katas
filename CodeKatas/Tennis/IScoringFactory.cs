using System;

namespace CodeKatas.Tennis
{
    internal class ScoringFactory
    {
        public IScore GetScoreStrategy(ScoringStrategies currentScore)
        {
            IScore strategy;

            switch (currentScore)
            {
                case ScoringStrategies.DrawUnderForty:
                    strategy =  new ScoreDrawUnderForty();
                    break;
                case ScoringStrategies.ScoreUnderForty:
                    strategy = new ScoreBothPlayersBelowForty();
                    break;
                case ScoringStrategies.Deuce:
                    strategy = new ScoreDeuce();
                    break;
                case ScoringStrategies.AdvantageForPlayer:
                    strategy = new ScoreAdvantage();
                    break;
                case ScoringStrategies.WinForPlayer:
                    strategy = new ScoreWin();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentScore), currentScore, null);
            }

            return strategy;
        }
    }
}
