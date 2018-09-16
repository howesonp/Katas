using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas
{
    public class BowlingGame
    {
        private const string Miss = "-";
        private static List<int> _scoreTracker;

        public int Score(string game)
        {
            _scoreTracker = null;
            _scoreTracker = new List<int>();

            var frames = game.Split('|');
            var totalScore = 0;
        
            for (int i = 0; i < frames.Length; i++)
            {
                totalScore += ScoreFrame(frames[i], i);
            }

            return totalScore;
        }

        private int ScoreFrame(string frame, int frameNumber)
        {
            if (string.IsNullOrEmpty(frame))
            {
                return 0;
            }

            var frameScore = 0;

            frameScore += ScoreRoll(frame.Substring(0, 1));
            frameScore += ScoreRoll(frame.Substring(1, 1));

            return frameScore;
        }

        private int ScoreRoll(string rollNumber)
        {
            if (rollNumber.IsMiss())
            {
                _scoreTracker.Add(0);
                return 0;
            }

            if (rollNumber.IsStrike() || rollNumber.IsSpare())
            {
                _scoreTracker.Add(10);
                return 10;
            }

            _scoreTracker.Add(int.Parse(rollNumber));
            return int.Parse(rollNumber);
        }
    }
}