namespace CodeKatas.Tennis
{
    internal class ScoreAdvantage : IScore
    {
        public string Score(Player playerOne, Player playerTwo)
        {
            if (playerOne.Score > playerTwo.Score)
            {
                return "Advantage " + playerOne.Name;
            }

            return "Advantage " + playerTwo.Name;
        }
    }
}