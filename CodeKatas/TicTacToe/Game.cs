namespace CodeKatas.TicTacToe
{
    public class Game
    {
        public Board Board;
        public GameState CurrentState;
        public PlayerSign PreviousPlayer;

        public Game()
        {
            InitialiseGameState();
            Board = new Board();
        }

        private void InitialiseGameState()
        {
            CurrentState = GameState.InProgress;
        }

        public void MakeMove(BoardPosition position, PlayerSign currentPlayer)
        {
            Board.CheckFirstPlayerIsNotNought(currentPlayer);
            CurrentState.IsGameOver();
            Board.PositionAlreadyTaken(position);
            currentPlayer.ValidateCorrectPlayer(PreviousPlayer);
            Board.UpdateSquare(currentPlayer, position);

            PreviousPlayer = currentPlayer;
        }

        public GameState CheckForResult(PlayerSign playerSign)
        {
            var isWin = IsWin(playerSign);

            if (isWin)
            {
                return PreviousPlayer == PlayerSign.Cross ? CurrentState = GameState.PlayerXWin : CurrentState = GameState.PlayerOWin;
            }

            return IsADraw() ? CurrentState = GameState.IsDraw : CurrentState = GameState.InProgress;
        }

        public bool IsADraw()
        {
            return Board.AreAllSquaresTaken();
        }

        public bool IsWin(PlayerSign playerSign)
        {
            return Board.CheckWinLines(playerSign);
        }
    }
}
