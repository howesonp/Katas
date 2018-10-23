using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class ResultChecker
    {
        private readonly Board _board;

        private readonly List<BoardSquarePosition[]> _winLines = new List<BoardSquarePosition[]>
        {
            new[]{ BoardSquarePosition.TopLeft, BoardSquarePosition.MiddleLeft, BoardSquarePosition.BottomLeft },
            new[]{ BoardSquarePosition.Top, BoardSquarePosition.Middle, BoardSquarePosition.Bottom },
            new[]{ BoardSquarePosition.TopRight, BoardSquarePosition.MiddleRight, BoardSquarePosition.BottomRight },
            new[]{ BoardSquarePosition.TopLeft, BoardSquarePosition.Top, BoardSquarePosition.TopRight },
            new[]{ BoardSquarePosition.MiddleLeft, BoardSquarePosition.Middle, BoardSquarePosition.MiddleRight },
            new[]{ BoardSquarePosition.BottomLeft, BoardSquarePosition.Bottom, BoardSquarePosition.BottomRight },
            new[]{ BoardSquarePosition.TopLeft, BoardSquarePosition.Middle, BoardSquarePosition.BottomRight },
            new[]{ BoardSquarePosition.TopRight, BoardSquarePosition.Middle, BoardSquarePosition.BottomLeft },
            new[]{ BoardSquarePosition.TopLeft, BoardSquarePosition.Top, BoardSquarePosition.TopRight }
        };

        public ResultChecker(Board board)
        {
            _board = board;
        }

        public bool CheckWinLines(PlayerSign playerSign)
        {
            return _winLines.Select(winLine => CheckLineForWin(winLine, playerSign))
                .Any(hasWin => hasWin);
        }

        private bool CheckLineForWin(BoardSquarePosition[] startOfWinLine, PlayerSign playerSign)
        {
            return _board.GetPlayerSignOnBoardPosition(startOfWinLine[0]) == playerSign &&
                   _board.GetPlayerSignOnBoardPosition(startOfWinLine[1]) == playerSign &&
                   _board.GetPlayerSignOnBoardPosition(startOfWinLine[2]) == playerSign;
        }
    }
}
