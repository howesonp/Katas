using System;
using System.Collections.Generic;
using System.Linq;

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
            if (Board.Squares.AreAllSquaresEmpty() && currentPlayer == PlayerSign.Nought)
            {
                throw new Exception("O cannot go first");
            }

            if (CurrentState == GameState.IsDraw || CurrentState == GameState.PlayerXWin || CurrentState == GameState.PlayerOWin)
            { 
                throw new Exception("Game over");
            }

            if (Board.Squares.IsSquareFilled(position))
            {
                throw new Exception("Position already filled");
            }

            if (currentPlayer == PreviousTurn)
            {
                throw new Exception($"{currentPlayer} took the previous turn");
            }
        }

        public void CheckForDraw()
        {
            var squaresFilled = Board.Squares.AreAllSquaresFilled();

            if (squaresFilled)
            {
                CurrentState = GameState.IsDraw;

            }
        }

        public void CheckForWin()
        {
            var isWin = CheckHorizontalWinLines() ||
                        CheckVerticalLinesForWin() ||
                        CheckDiagonalLinesForWin();

            if (isWin)
            {
                CurrentState =  PreviousTurn == PlayerSign.Nought ? GameState.PlayerOWin : GameState.PlayerXWin;
            }
        }

        private bool CheckHorizontalWinLines()
        {
            const int horizontalWinLineAddition = 1;
            return CheckWinLines(Board.WinningHorizontalLines, horizontalWinLineAddition);
        }

        private bool CheckVerticalLinesForWin()
        {
            const int verticalWinLineAddition = 3;
            return CheckWinLines(Board.WinningVerticalLines, verticalWinLineAddition);
        }

        private bool CheckDiagonalLinesForWin()
        {
            const int firstDiagonalLineAddition = 4;
            const int secondDiagonalLineAddition = 2;

            var firstDiagonalHasWin = CheckWinLines(Board.WinningDiagonalLineOne, firstDiagonalLineAddition);
            var secondDiagonalHasWin = CheckWinLines(Board.WinningDiagonalLineTwo, secondDiagonalLineAddition);

            return firstDiagonalHasWin || secondDiagonalHasWin;
        }

        private bool CheckWinLines(IEnumerable<BoardPosition> winLines, int addition)
        {
            return winLines.Select(winLine => CheckLineForWin(winLine, addition))
                           .Any(hasWin => hasWin);
        }

        private bool CheckLineForWin(BoardPosition startOfWinLine, int addition)
        {
            return Board.Squares.GetPlayerSignOnBoardPosition(startOfWinLine) == PreviousTurn &&
                   Board.Squares.GetPlayerSignOnBoardPosition(startOfWinLine + addition) == PreviousTurn &&
                   Board.Squares.GetPlayerSignOnBoardPosition(startOfWinLine + addition + addition) == PreviousTurn;
        }
    }
}
