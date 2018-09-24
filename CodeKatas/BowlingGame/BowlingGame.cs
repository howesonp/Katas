using System.Linq;

namespace CodeKatas
{
    public class BowlingGame
    {
        private const int MissScore = 0;
        private const int StrikeScore = 10;
        
        private static Game _game;

        public int Score(string game)
        {
            _game = new Game();

            var removeDoubleSlashInFrames = game.Replace("||", "|");
            GetFrameScores(removeDoubleSlashInFrames, _game);
            var totalScoreNew = GetTotalFrameScore(_game);

            return totalScoreNew;
        }

        private void GetFrameScores(string frameScoresAsString, Game game)
        {
            var stringFrameScoreArray = frameScoresAsString.Split('|').Take(11).ToArray();

            for (var frameNumber = 0; frameNumber < stringFrameScoreArray.Length; frameNumber++)
            {
                var frame = stringFrameScoreArray[frameNumber];
                var currentFrame = GetFrameScore(frame, frameNumber);

                game.Frames.Add(currentFrame);
            }
        }

        private Frame GetFrameScore(string frame, int frameNumber)
        {
            var firstThrow = GetFirstThrowScore(frame, frameNumber);

            var secondThrow = GetSecondThrowScore(frame, frameNumber, firstThrow.Score);

            var currentFrame = new Frame
            {
                FrameNumber = frameNumber,
                FirstThrow = firstThrow,
                IsLastFrame = frameNumber == 10,
                SecondThrow = secondThrow,
            };
            return currentFrame;
        }

        private int GetTotalFrameScore(Game game)
        {
            var totalScore = 0;

            for (var i = 0; i < game.Frames.Count; i++)
            {
                if (game.Frames[i].HasStrike)
                { 
                    var score = game.Frames[i].FrameScore + game.NextTwoScoringThrows(i);

                    totalScore += score;
                }

                else if (game.Frames[i].SecondThrow.StringThrow.IsSpare())
                {
                    totalScore += game.Frames[i].FrameScore + game.NextScoringThrow(i);
                }
                else
                {
                    totalScore += game.Frames[i].FrameScore;
                }
            }

            return totalScore;
        }

        private static Throw GetFirstThrowScore(string frame, int frameNumber)
        {
            var firstThrow = frame.GetFirstThrow();
            var firstThrowScore = 0;

            if (firstThrow.IsMiss())
            {
                return new Throw 
                {
                    FrameNumber = frameNumber,
                    Score = MissScore,
                    StringThrow = firstThrow
                };
            }

            if (firstThrow.IsStrike())
            {
                return new Throw
                {
                    FrameNumber = frameNumber,
                    Score = StrikeScore,
                    StringThrow = firstThrow
                };
            }
            firstThrowScore = int.Parse(firstThrow);

            return new Throw
            {
                FrameNumber = frameNumber,
                Score = firstThrowScore,
                StringThrow = firstThrow
            };
        }

        private static Throw GetSecondThrowScore(string frame, int frameNumber, int firstThrowScore)
        {
            var secondThrow = frame.GetSecondThrow();

            if (secondThrow.IsMiss())
            {
                return new Throw
                {
                    FrameNumber = frameNumber,
                    Score = MissScore,
                    StringThrow = secondThrow
                };
            }

            if (secondThrow.IsSpare())
            {
                return new Throw
                {
                    FrameNumber = frameNumber,
                    Score = StrikeScore - firstThrowScore,
                    StringThrow = secondThrow
                };
            }
            return new Throw
            {
                FrameNumber = frameNumber,
                Score = int.Parse(secondThrow),
                StringThrow = secondThrow
            };
        }
    }
}