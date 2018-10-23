using System.Collections.Generic;

namespace CodeKatas.TicTacToe
{
    internal static class GameWinOptions
    {
        public static List<BoardPosition[]> Lines = new List<BoardPosition[]>
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
    }
}
