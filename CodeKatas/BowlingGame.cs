using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace CodeKatas
{
    public class BowlingGame
    {
        //private const string Miss = "-";
        private const int MissScore = 0;
        private const int StrikeScore = 10;
        //private static Dictionary<int, Frame> _scoreTracker;

        private static Game _runningScore;

        public int Score(string game)
        {
            //_scoreTracker = null;
            //_scoreTracker = new Dictionary<int, Frame>();

            _runningScore = null;
            _runningScore = new Game();

            var removeDoubleSlashInFrames = game.Replace("||", "|");
            //var frames = removeDoubleSlashInFrames.Split('|').Take(10).ToArray();
            GetFrameScores(removeDoubleSlashInFrames, ref _runningScore);
            var totalScoreNew = GetTotalFrameScore(ref _runningScore);

            //var bonusFrame = frames.Skip(10).Take(1);

            //var totalScore = 0;
        
            //ScoreAllFrames(frames);
            //AddAllAdditionalPoints(frames);

            //foreach (var frame in _scoreTracker)
            //{
            //    //totalScore += frame.Value.FrameScore;
            //    totalScore += frame.Value.AccumulatedFrameScore;
            //}

            return totalScoreNew;
        }

        private void  GetFrameScores(string frameScoresAsString, ref Game _runningScore)
        {
            var stringFrameScoreArray = frameScoresAsString.Split('|').Take(10).ToArray();

            for (var frameNumber = 0; frameNumber < stringFrameScoreArray.Length; frameNumber++)
            {
                var frame = stringFrameScoreArray[frameNumber];
                var firstThrow = GetFirstThrowScore(frame, frameNumber);
     
                var secondThrow = GetSecondThrowScore(frame, frameNumber, firstThrow.Score);

                var currentFrame = new Frame
                {
                    FrameNumber = frameNumber,
                    FirstThrow = firstThrow,
                    SecondThrow = secondThrow
                };

                _runningScore.Frames.Add(currentFrame);
            }
        }

        private int GetTotalFrameScore(ref Game runningScore)
        {
            var totalScore = 0;

            for (int i = 0; i < runningScore.Frames.Count; i++)
            {
                if (runningScore.Frames[i].HasStrike)
                {
                    totalScore += runningScore.Frames[i].FrameScore + runningScore.NextTwoScoringThrows(i);
                }

                else if (runningScore.Frames[i].SecondThrow.StringThrow.IsSpare())
                {
                    totalScore += runningScore.Frames[i].FrameScore + runningScore.NextScoringThrow(i);
                }
                else
                {
                    totalScore += runningScore.Frames[i].FrameScore;
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

            //return firstThrowScore;
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

    public class Frame
    {
        public Frame()
        {
            FirstThrow = new Throw();
            SecondThrow = new Throw();
        }

        public int FrameNumber { get; set; }

        public Throw FirstThrow { get; set; }
        public Throw SecondThrow { get; set; }

        public int FrameScore => FirstThrow.Score + SecondThrow.Score;

        public bool HasStrike => FirstThrow.Score == 10;
    }

    public class Game
    {
        public Game()
        {
            Frames = new List<Frame>();
        }

        public List<Frame> Frames { get; set; }
    }

    public class Throw
    {
        public int FrameNumber { get; set; }
        public string StringThrow { get; set; }
        public int Score { get; set; }
    }
}