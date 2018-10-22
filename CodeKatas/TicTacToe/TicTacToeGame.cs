namespace CodeKatas.TicTacToe
{
    public class TicTacToeGame
    {
        private readonly Board Board;
        private GameWinOptions ResultChecker;
        public GameState gameState;

        public TicTacToeGame()
        {
            Board = new Board();
            ResultChecker = new GameWinOptions();
            gameState = GameState.InProgress;
        }

        public GameState TryToTakeTurn(Move move)
        {
            gameState.CheckIfIsGameOver();
            Board.AddMoveToBoard(move);
            gameState = Board.CheckForResult();
            return gameState;
        }
    }
}

