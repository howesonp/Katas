using System;

namespace CodeKatas.TicTacToe
{
    public class Game
    {
        public Board Board;
        public GameState CurrentState;
        public PlayerSign PreviousTurn;

        public Game()
        {
            InitialiseGameState();
            Board = new Board();
        }

        private void InitialiseGameState()
        {
            CurrentState = GameState.InProgress;
        }

        public void CheckIfMoveValid(BoardPosition position, PlayerSign currentPlayer)
        {
            if (Board.AreAllSquaresEmpty() && currentPlayer == PlayerSign.Nought)
            {
                throw new Exception("O cannot go first");
            }

            if (CurrentState == GameState.IsDraw || CurrentState == GameState.PlayerXWin || CurrentState == GameState.PlayerOWin)
            { 
                throw new Exception("Game over");
            }

            if (Board.IsSquareFilled(position))
            {
                throw new Exception("Position already filled");
            }

            if (currentPlayer == PreviousTurn)
            {
                throw new Exception($"{currentPlayer} took the previous turn");
            }
        }

        public GameState CheckForResult(PlayerSign playerSign)
        {
            var result = IsWin(playerSign);

            if (result)
            {
                return PreviousTurn == PlayerSign.Cross ? CurrentState = GameState.PlayerXWin : CurrentState = GameState.PlayerOWin;
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
