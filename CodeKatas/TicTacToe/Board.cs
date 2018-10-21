using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Board
    {
        public SquareCollection Squares;

        public List<BoardPosition[]> WinLines = new List<BoardPosition[]>
        {
            new[]{ BoardPosition.TopLeft, BoardPosition.MiddleLeft, BoardPosition.BottomLeft },
            new[]{ BoardPosition.Top, BoardPosition.Middle, BoardPosition.Bottom },
            new[]{ BoardPosition.TopRight, BoardPosition.MiddleRight, BoardPosition.BottomRight },
            new[]{ BoardPosition.TopLeft, BoardPosition.Top, BoardPosition.TopRight },
            new[]{ BoardPosition.MiddleLeft, BoardPosition.Middle, BoardPosition.MiddleRight },
            new[]{ BoardPosition.BottomLeft, BoardPosition.Bottom, BoardPosition.BottomRight },
            new[]{ BoardPosition.TopLeft, BoardPosition.Middle, BoardPosition.BottomRight },
            new[]{ BoardPosition.TopRight, BoardPosition.Middle, BoardPosition.BottomLeft },
            new[]{ BoardPosition.TopLeft, BoardPosition.Top, BoardPosition.TopRight },
        };

        public Board()
        {
            Squares = new SquareCollection();
        }

        public bool IsSquareFilled(BoardPosition position)
        {
            return Squares.IsSquareTaken(position);
        }

        public bool AreAllSquaresEmpty()
        {
            return Squares.AreAllEmpty();
        }

        public bool AreAllSquaresTaken()
        {
            return Squares.AreAllTaken();
        }

        public void UpdateSquare(PlayerSign playerSign, BoardPosition boardPosition)
        {
            Squares.UpdateSquare(boardPosition, playerSign);
        }

        public bool CheckWinLines(PlayerSign playerSign)
        {
            return WinLines.Select(winLine => CheckLineForWin(winLine, playerSign))
                .Any(hasWin => hasWin);
        }

        private bool CheckLineForWin(BoardPosition[] startOfWinLine, PlayerSign playerSign)
        {
            return Squares.GetPlayerSignOnBoardPosition(startOfWinLine[0]) == playerSign &&
                   Squares.GetPlayerSignOnBoardPosition(startOfWinLine[1]) == playerSign &&
                   Squares.GetPlayerSignOnBoardPosition(startOfWinLine[2]) == playerSign;
        }
    }
}
