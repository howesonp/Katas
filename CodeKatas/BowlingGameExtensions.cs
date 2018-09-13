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
    }
}
