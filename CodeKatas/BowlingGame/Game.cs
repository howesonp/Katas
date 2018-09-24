using System.Collections.Generic;

namespace CodeKatas
{
    public class Game
    {
        public Game()
        {
            Frames = new List<Frame>();
        }

        public List<Frame> Frames { get; set; }

        public int GameScore { get; set; }
    }
}