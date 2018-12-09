namespace CodeSmellsKata.TennisGame
{
    public interface ITennisGame
    {
        void WonPoint(string playerName);
        string GetScore();
    }
}
