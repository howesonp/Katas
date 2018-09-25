namespace CodeKatas
{
    public class BowlingGame
    {
        private static ScoreCard _scoreCard;

        public int Score(string stringGame)
        {
            _scoreCard = new ScoreCard();
            _scoreCard.ReplaceDoublePipeInStringGame(stringGame);
            _scoreCard.CreateAllFramesFromString();
            _scoreCard.GetTotalFrameScoreIncludingBonus();

            return _scoreCard.Score;
        }
    }
}