namespace CodeKatas
{
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
        public bool HasSpare => SecondThrow.StringThrow == "/";
        public bool IsLastFrame { get; set; }
    }
}