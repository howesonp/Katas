using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class ResultChecker
    {
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

        public GameState CheckForResult(Board board)
        {
            var isWin = CheckWinLines(board);

            if (isWin)
            {
                return board.lastPlayed == PlayerSign.Cross ? GameState.PlayerXWin : GameState.PlayerOWin;
            }

            return IsDraw(board) ? GameState.IsDraw : GameState.InProgress;
        }


        public bool CheckWinLines(Board board)
        {

            return WinLines.Select(winLine =>
                            board.GetPlayerSignOnBoardPosition(winLine[0]) == board.lastPlayed &&
                            board.GetPlayerSignOnBoardPosition(winLine[1]) == board.lastPlayed &&
                            board.GetPlayerSignOnBoardPosition(winLine[2]) == board.lastPlayed)
                    .Any(hasWin => hasWin);
        }

        public bool IsDraw(Board board)
        {
            return board.AreAllMovesTaken();
        }
    }
}
