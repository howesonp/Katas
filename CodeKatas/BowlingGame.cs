using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodeKatas
{
    public class BowlingGame
    {
        private const string Miss = "-";
        private static Dictionary<int, Frame> _scoreTracker;

        public int Score(string game)
        {
            _scoreTracker = null;
            _scoreTracker = new Dictionary<int, Frame>();

            var removeDoubleSlashInFrames = game.Replace("||", "|");
            var frames = removeDoubleSlashInFrames.Split('|').Take(10).ToArray();
            var bonusFrame = frames.Skip(10).Take(1);

            var totalScore = 0;
        
            ScoreAllFrames(frames);
            AddStrikesAdditionalPoints(frames);

            foreach (var frame in _scoreTracker)
            {
                totalScore += frame.Value.FrameScore;
            }

            return totalScore;
        }

        private void ScoreAllFrames(string[] frames)
        {
            for (int frame = 0; frame < frames.Length; frame++)
            {
                ScoreFrame(frames[frame], frame);
            }
        }

        private void AddStrikesAdditionalPoints(string[] frames)
        {
            for (var frame = 0; frame < frames.Length; frame++)
            {
                AddAdditionalPoints(ref _scoreTracker, _scoreTracker[frame]);
            }
        }

        private void ScoreFrame(string frame, int frameNumber)
        {
            if (string.IsNullOrEmpty(frame))
            {
                return;
            }

            var currentFrame = new Frame
            {
                FrameNumber = frameNumber,
                FirstThrow = frame.GetFirstThrow(),
                SecondThrow = frame.GetSecondThrow()
            };

            ScoreFirstRoll(ref currentFrame);
            ScoreSecondRoll(ref currentFrame);

            _scoreTracker.Add(frameNumber, currentFrame);
        }

        private void ScoreFirstRoll(ref Frame currentFrame)
        {
            if (currentFrame.FirstThrow.IsMiss())
            {
                currentFrame.FirstThrowScore = 0;
            }
            else if (currentFrame.FirstThrow.IsStrike())
            {
                currentFrame.HasStrike = true;
                currentFrame.FirstThrowScore = 10;
            }
            else
            {
                currentFrame.FirstThrowScore = int.Parse(currentFrame.FirstThrow);
            }
        }

        private void ScoreSecondRoll(ref Frame currentFrame)
        {
            if (currentFrame.HasStrike)
            {
                return;
            }

            if (currentFrame.SecondThrow.IsMiss())
            {
                currentFrame.SecondThrowScore = 0;
            }
            else if (currentFrame.SecondThrow.IsSpare())
            {
                currentFrame.HasSpare = true;
                currentFrame.SecondThrowScore = 10;
            }
            else
            {
                currentFrame.SecondThrowScore = int.Parse(currentFrame.SecondThrow);
            }
        }

        private void AddAdditionalPoints(ref Dictionary<int, Frame> scoreTracker, Frame currentFrame)
        {
            var currentFrameScore = scoreTracker[currentFrame.FrameNumber];
            if (currentFrame.FrameNumber <= 8)
            {
                if (currentFrameScore.FirstThrow.IsStrike())
                {
                    currentFrameScore.FirstThrowScore += AddNextTwoScoringRolls(scoreTracker, currentFrame);

                }
                if (currentFrameScore.SecondThrow.IsSpare())
                {
                    currentFrameScore.SecondThrowScore += AddNextOneScoringRoll(scoreTracker, currentFrame);
                }
            }
        }

        private int AddNextTwoScoringRolls(Dictionary<int, Frame> scoreTracker, Frame currentFrame)
        {
            var returnNumber = 0;
            var remainingFrames = scoreTracker.Skip(currentFrame.FrameNumber + 1).Take(2);

            foreach (var frame in remainingFrames)
            {
                var thisFrame = frame.Value;

                if (thisFrame.HasStrike)
                {
                    returnNumber += 10;
                }
                else
                {
                    returnNumber += returnNumber == 10 ? thisFrame.FirstThrowScore : thisFrame.FrameScore;

                    break;
                }
            }

            return returnNumber;
        }

        private int AddNextOneScoringRoll(Dictionary<int, Frame> scoreTracker, Frame currentFrame)
        {
            var nextFrame = scoreTracker.Skip(currentFrame.FrameNumber + 1).Take(1);

            return nextFrame.FirstOrDefault().Value.FirstThrowScore;
        }
    }

    public class Frame
    {
        public int FrameNumber { get; set; }
        public string FirstThrow{ get; set; }
        public int FirstThrowScore{ get; set; }
        public string SecondThrow { get; set; }
        public int SecondThrowScore { get; set; }
        public bool HasStrike { get; set; }
        public bool HasSpare { get; set; }
        public int FrameScore => FirstThrowScore + SecondThrowScore;
    }

    public class Game
    {
        public List<Frame> Frames { get; set; }
    }
}