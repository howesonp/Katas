namespace CodeKatas.Tennis
{
    internal class ScoreBothPlayersBelowForty : IScore
    {
        public string Score(Player playerOne, Player playerTwo)
        {
            return ScoresUnderForty.ScoreLabelsUpToForty[playerOne.Score] 
                   + "-" 
                   + ScoresUnderForty.ScoreLabelsUpToForty[playerTwo.Score];
        }
    }
}