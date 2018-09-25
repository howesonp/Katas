namespace CodeKatas
{
    public class Throw
    {
        public Throw(int frameNumber, int score, string stringThrow)
        {
            FrameNumber = frameNumber;
            Score = score;
            StringThrow = stringThrow;
        }

        public int FrameNumber { get;  }
        public string StringThrow { get; }
        public int Score { get; }
    }
}