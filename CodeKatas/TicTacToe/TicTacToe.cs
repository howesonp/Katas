namespace CodeKatas.TicTacToe
{
    public class TicTacToe
    {
        public readonly Game Game;
        public Player PlayerX;
        public Player PlayerO;

        public TicTacToe()
        {
            Game = new Game();
            PlayerX = new Player(PlayerSign.Cross, Game);
            PlayerO = new Player(PlayerSign.Nought, Game);
        }
    }
}

