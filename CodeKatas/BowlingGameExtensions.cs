using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas
{
    public static class BowlingGameExtensions
    {
        private const string MISS = "-";
        private const string SPARE = "/";
        private const string STRIKE = "X";

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

        public static int NextScoringThrow(this Game runningScore, int currentFrame)
        {
            var concurrentScores = new List<int>();
    
            var remainingFrames = runningScore.Frames.Where(e => e.FrameNumber > currentFrame).ToList();

            remainingFrames.ForEach(frame =>
            {
                concurrentScores.Add(frame.FirstThrow.Score);
                concurrentScores.Add(frame.SecondThrow.Score);
            });

            //var removeFirstThrowMisses = concurrentScores.Where(e => e != 0);

            return concurrentScores.FirstOrDefault();

        }

        public static int NextTwoScoringThrows(this Game runningScore, int currentFrame)
        {
            var scoreToAdd = 0;
            var remainingFrames = runningScore.Frames.Where(e => e.FrameNumber > currentFrame).ToList();

            foreach (var frame in remainingFrames)
            {
                scoreToAdd += frame.FirstThrow.Score;

                if (frame.HasStrike)
                {
                    scoreToAdd += remainingFrames[1 + 1].FirstThrow.Score;
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
