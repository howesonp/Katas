using System.Collections.Generic;

namespace CodeKatas
{
    public class ScoreCard
    {
        public ScoreCard()
        {
            Frames = new List<Frame>();
        }

        public List<Frame> Frames { get; set; }

        public int Score { get; set; }

        public string StringGame;
    }
}