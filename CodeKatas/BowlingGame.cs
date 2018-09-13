using System.Linq;

namespace CodeKatas
{
    public class BowlingGame
    {
        private const string Miss = "-";

        public int Score(string game)
        {

            var frames = game.Split('|');

            return frames.Sum(ScoreFrame);
        }

        private static int ScoreFrame(string game)
        {
            if (string.IsNullOrEmpty(game))
            {
                return 0;
            }

            var firstHit = game.Substring(0, 1);
            var secondHit = game.Substring(1, 1);

            if (firstHit.IsMiss() && secondHit.IsMiss())
            {
                return 0;
            }

            if (firstHit.IsSpare() || secondHit.IsSpare())
            {
                return 10;
            }

            if (firstHit.IsStrike() || secondHit.IsStrike())
            {
                return 10;
            }

            if (secondHit.IsMiss())
            {
                return int.Parse(firstHit);
            }

            if (firstHit.IsMiss())
            {
                return int.Parse(secondHit);
            }

            return int.Parse(firstHit) + int.Parse(secondHit);
        }
    }
}