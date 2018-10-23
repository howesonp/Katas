namespace CodeKatas.TicTacToe
{
    public class TicTacToeGame
    {
        private readonly Board Board;

        public TicTacToeGame()
        {
            Board = new Board();
        }

        public GameState TryToTakeTurn(Move move)
        {
            Board.AddMoveToBoard(move);
            return Board.CheckForResult();
        }
    }
}

