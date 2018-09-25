namespace CodeKatas
{
    public class BowlingGame
    {
        private static ScoreCard _scoreCard;

        public int Score(string game)
        {
            _scoreCard = new ScoreCard();
            _scoreCard.ReplaceDoublePipeInStringGame(game);
            _scoreCard.ScoresAllFramesAsNumbers();
            _scoreCard.GetTotalFrameScore();

            return _scoreCard.Score;
        }
    }
}