namespace CodeKatas.TicTacToe
{
    public class TicTacToeGame
    {
        public readonly Board Board;
        private ResultChecker ResultChecker;
        private GameState gameState;

        public TicTacToeGame()
        {
            Board = new Board();
            ResultChecker = new ResultChecker();
            gameState = GameState.InProgress;
        }

        public void TryToTakeTurn(BoardPosition position, PlayerSign playerSign)
        {
            gameState.CheckIfIsGameOver();
            Board.AddMoveToBoard(position, playerSign);
        }

        public GameState CheckGameForResult()
        {
            gameState =  ResultChecker.CheckForResult(Board);
            return gameState;
        }
    }
}

