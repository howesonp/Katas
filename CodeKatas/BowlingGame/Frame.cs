namespace CodeKatas
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        public Throw FirstThrow { get; set; }
        public Throw SecondThrow { get; set; }
        public int FrameScore => FirstThrow.Score + SecondThrow.Score;
        public bool HasStrike => FirstThrow.Score == 10;
        public bool IsBonusFrame => FrameNumber == 10;
    }
}