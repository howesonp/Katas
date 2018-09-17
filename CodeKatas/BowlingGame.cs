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

        private int ScoreRoll(string pinsHitOnRoll)
        { 
            if (pinsHitOnRoll.IsMiss())
            {
                _scoreTracker.Add(0);
                return 0;
            }

            if (pinsHitOnRoll.IsStrike() || pinsHitOnRoll.IsSpare())
            {
                _scoreTracker.Add(10);
                //AddToPreviousStikeOrSpareIfRequired(_scoreTracker, 10);

                return 10;
            }

            var score = int.Parse(pinsHitOnRoll);

            _scoreTracker.Add(int.Parse(pinsHitOnRoll));
            return int.Parse(pinsHitOnRoll);
        }

        private void AddToPreviousStikeOrSpareIfRequired(ref List<int> scoreTracker, int i)
        {
            
        }
    }
}