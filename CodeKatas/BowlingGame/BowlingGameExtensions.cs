using System.Collections.Generic;
using System.Linq;

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

        public static void CreateAllFramesFromString(this ScoreCard scoreCard)
        {
            var stringFrameScoreArray = scoreCard.StringGame.Split('|').Take(11).ToArray();

            for (var frameNumber = 0; frameNumber < stringFrameScoreArray.Length; frameNumber++)
            {
                var stringFrame = stringFrameScoreArray[frameNumber];
                var currentFrame = CreateFrame(stringFrame, frameNumber);

                scoreCard.Frames.Add(currentFrame);
            }
        }

        private static Frame CreateFrame(string frame, int frameNumber)
        {
            var firstThrow = GetFirstThrow(frame, frameNumber);

            var secondThrow = GetSecondThrow(frame, frameNumber, firstThrow.Score);

            var currentFrame = new Frame 
            {
                FrameNumber = frameNumber,
                FirstThrow = firstThrow,
                SecondThrow = secondThrow,
            };
            return currentFrame;
        }

        private static Throw GetFirstThrow(string frame, int frameNumber)
        {
            var firstThrow = frame.GetFirstThrowString();

            if (firstThrow.IsMiss())
            {
                return new Throw(frameNumber, MissScore, firstThrow);
            }

            return firstThrow.IsStrike()
                ? new Throw(frameNumber, StrikeScore, firstThrow)
                : new Throw(frameNumber, int.Parse(firstThrow), firstThrow);
        }

        private static Throw GetSecondThrow(string frame, int frameNumber, int firstThrowScore)
        {
            var secondThrow = frame.GetSecondThrowString();

            if (secondThrow.IsMiss())
            {
                return new Throw(frameNumber, MissScore, secondThrow);
            }

            if (secondThrow.IsSpare())
            {
                return new Throw(frameNumber, StrikeScore - firstThrowScore, secondThrow);
            }

            return secondThrow.IsStrike()
                ? new Throw(frameNumber, StrikeScore, secondThrow)
                : new Throw(frameNumber, int.Parse(secondThrow), secondThrow);
        }

        public static void GetTotalFrameScoreIncludingBonus(this ScoreCard scoreCard)
        {
            scoreCard.Score = 0;

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
            var remainingFrames = scoreCard.Frames.Where(e => e.FrameNumber > currentFrame).OrderBy(frame =>frame.FrameNumber).ToList();

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

        public static string GetFirstThrowString(this string frame)
        {
            return frame.Substring(0, 1);
        }

        public static string GetSecondThrowString(this string frame)
        {
            return frame.Substring(1, 1);

        }
    }
}
