namespace CodeKatas.Tennis
{
    internal class ScoreWin : IScore
    {
        public string Score(Player playerOne, Player playerTwo)
        {
            if (playerOne.Score > playerTwo.Score)
            {
                return "Win for " + playerOne.Name;
            }

            return "Win for " + playerTwo.Name;
        }
    }
}