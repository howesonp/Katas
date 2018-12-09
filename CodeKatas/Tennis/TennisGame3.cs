using CodeSmellsKata.TennisGame;

namespace CodeKatas.Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;

        public TennisGame3(string player1Name, string player2Name)
        {
            _playerOne = new Player {Name = player1Name};
            _playerTwo = new Player {Name = player2Name};
        }

        public string GetScore()
        {
            var scoringFactory = new ScoringFactory();

            if (IsDrawUnderForty())
            {
                var draw = scoringFactory.GetScoreStrategy(ScoringStrategies.DrawUnderForty);

                return draw.Score(_playerOne, _playerTwo);
            }

            if (IsNotDrawUnderForty())
            {
                var scoreUnderForty = scoringFactory.GetScoreStrategy(ScoringStrategies.ScoreUnderForty);

                return scoreUnderForty.Score(_playerOne, _playerTwo);
            }

            if (IsDeuce())
            {
                var deuce = scoringFactory.GetScoreStrategy(ScoringStrategies.Deuce);

                return deuce.Score(_playerOne, _playerTwo);
            }

            if (IsAdvantageForWinningPlayer())
            {
                var advantage = scoringFactory.GetScoreStrategy(ScoringStrategies.AdvantageForPlayer);

                return advantage.Score(_playerOne, _playerTwo);
            }

            var win = scoringFactory.GetScoreStrategy(ScoringStrategies.WinForPlayer);

            return win.Score(_playerOne, _playerTwo);
        }

        private bool IsDeuce()
        {
            return IsDrawCurrently() && !IsGameScoreUnderForty();
        }

        private bool IsNotDrawUnderForty()
        {
            return !IsDrawCurrently() && IsGameScoreUnderForty();
        }

        private bool IsDrawUnderForty()
        {
            return IsDrawCurrently() && IsGameScoreUnderForty();
        }

        private bool IsDrawCurrently()
        {
            return _playerOne.Score == _playerTwo.Score;
        }

        private bool IsAdvantageForWinningPlayer()
        {
            return (_playerOne.Score - _playerTwo.Score) * (_playerOne.Score - _playerTwo.Score) == 1;
        }

        private bool IsGameScoreUnderForty()
        {
            return (_playerOne.Score < 4 && _playerTwo.Score < 4) && (_playerOne.Score + _playerTwo.Score < 6);
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
            {
                _playerOne.Score += 1;
            }
            else
            {
                _playerTwo.Score += 1;
            }
        }

    }
}
