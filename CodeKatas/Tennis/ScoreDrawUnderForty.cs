namespace CodeKatas.Tennis
{
    internal class ScoreDrawUnderForty : IScore
    {
        public string Score(Player playerOne, Player playerTwo)
        {
            return ScoresUnderForty.ScoreLabelsUpToForty[playerOne.Score] + "-All";
        }
    }
}