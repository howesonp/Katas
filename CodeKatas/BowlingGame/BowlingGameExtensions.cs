using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CodeKatas
{
    public static class BowlingGameExtensions
    {
        private const string MISS = "-";
        private const string SPARE = "/";
        private const string STRIKE = "X";
        private const int MissScore = 0;
        private const int StrikeScore = 10;

        public static void ReplaceDoublePipeInStringGame(this ScoreCard scoreCard, string stringGame)
        {
            scoreCard.StringGame = stringGame.Replace("||", "|");
        }

        public static void GetFrameScoresAsNumbers(this ScoreCard scoreCard)
        {
            var stringFrameScoreArray = scoreCard.StringGame.Split('|').Take(11).ToArray();

            for (var frameNumber = 0; frameNumber < stringFrameScoreArray.Length; frameNumber++)
            {
                var frame = stringFrameScoreArray[frameNumber];
                var currentFrame = GetFrameScore(frame, frameNumber);

                scoreCard.Frames.Add(currentFrame);
            }
        }

        public static void GetTotalFrameScore(this ScoreCard scoreCard)
        {
            scoreCard.Score = 0;

            //scoreCard.Frames.ForEach(frame =>
            //{
            //    if (frame.IsLastFrame && !(frame.HasStrike || frame.HasSpare))
            //    {
            //        return;
            //    }

            //    if (frame.HasStrike)
            //    {
            //        scoreCard.Score = frame.FrameScore + scoreCard.NextTwoScoringThrows(frame.FrameNumber);
            //    }

            //    else if (frame.SecondThrow.StringThrow.IsSpare())
            //    {
            //        scoreCard.Score += frame.FrameScore + scoreCard.NextScoringThrow(frame.FrameNumber);
            //    }
            //    else
            //    {
            //        scoreCard.Score += frame.FrameScore;
            //    }
            //});

            foreach (var currentFrame in scoreCard.Frames)
            {
                if (!currentFrame.IsBonusFrame)
                {
                    scoreCard.ScoreCurrentFrame(currentFrame);
                }
            }
        }

        private static void ScoreCurrentFrame(this ScoreCard scoreCard, Frame currentFrame)
        {
            if (currentFrame.HasStrike)
            {
                scoreCard.ScoreStrike(currentFrame);
            }

            else if (currentFrame.SecondThrow.StringThrow.IsSpare())
            {
                scoreCard.ScoreSpare(currentFrame);
            }
            else
            {
                scoreCard.Score += currentFrame.FrameScore;
            }
        }

        private static void ScoreSpare(this ScoreCard scoreCard, Frame currentFrame)
        {
            scoreCard.Score += currentFrame.FrameScore + scoreCard.NextScoringThrow(currentFrame.FrameNumber);
        }

        private static void ScoreStrike(this ScoreCard scoreCard, Frame currentFrame)
        {
            var score = currentFrame.FrameScore + scoreCard.NextTwoScoringThrows(currentFrame.FrameNumber);

            scoreCard.Score += score;
        }

        private static Frame GetFrameScore(string frame, int frameNumber)
        {
            var firstThrow = GetFirstThrowScore(frame, frameNumber);

            var secondThrow = GetSecondThrowScore(frame, frameNumber, firstThrow.Score);

            var currentFrame = new Frame
            {
                FrameNumber = frameNumber,
                FirstThrow = firstThrow,
                IsBonusFrame = frameNumber == 10,
                SecondThrow = secondThrow,
            };
            return currentFrame;
        }

        private static Throw GetFirstThrowScore(string frame, int frameNumber)
        {
            var firstThrow = frame.GetFirstThrow();

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

            return new Throw
            {
                FrameNumber = frameNumber,
                Score = int.Parse(firstThrow),
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

            if (secondThrow.IsStrike())
            {
                return new Throw
                {
                    FrameNumber = frameNumber,
                    Score = StrikeScore,
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

        public static bool IsMiss(this string ballThrow)
        {
            return ballThrow == MISS;
        }

        public static bool IsStrike(this string ballThrow)
        {
            return ballThrow == STRIKE;
        }

        public static bool IsSpare(this string ballThrow)
        {
            return ballThrow == SPARE;
        }

        public static string GetFirstThrow(this string frame)
        {
            return frame.Substring(0, 1);
        }

        public static string GetSecondThrow(this string frame)
        {
            return frame.Substring(1, 1);

        }

        public static int NextScoringThrow(this ScoreCard scoreCard, int currentFrame)
        {
            var concurrentScores = new List<int>();
    
            var remainingFrames = scoreCard.Frames.Where(e => e.FrameNumber > currentFrame).ToList();

            remainingFrames.ForEach(frame =>
            {
                concurrentScores.Add(frame.FirstThrow.Score);
                concurrentScores.Add(frame.SecondThrow.Score);
            });

            return concurrentScores.FirstOrDefault();
        }

        public static int NextTwoScoringThrows(this ScoreCard scoreCard, int currentFrame)
        {
            var scoreToAdd = 0;
            var remainingFrames = scoreCard.Frames.Where(e => e.FrameNumber > currentFrame).ToList();

            foreach (var frame in remainingFrames)
            {
                scoreToAdd += frame.FirstThrow.Score;

                if (frame.HasStrike && !frame.IsBonusFrame)
                {
                    scoreToAdd += remainingFrames[1].FirstThrow.Score;
                }
                else
                {
                    scoreToAdd += frame.SecondThrow.Score;
                }

                break;
            }

            return scoreToAdd;
        }
    }
}
