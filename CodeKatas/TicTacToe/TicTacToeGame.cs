namespace CodeKatas.TicTacToe
{
    public class TicTacToeGame
    {
        private readonly Board Board;
        public GameState GameState;

        public TicTacToeGame()
        {
            Board = new Board();
            GameState = GameState.InProgress;
        }

        public GameState TryToTakeTurn(Move move)
        {
            GameState.CheckIfIsGameOver();
            Board.AddMoveToBoard(move);
            GameState = Board.CheckForResult();
            return GameState;
        }
    }
}

