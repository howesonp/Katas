using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas.TicTacToe
{
    public static class SquareExtensions
    {
        public static bool PositionIsAlreadyTaken(this SquareCollection squares, BoardPosition position)
        {
            return squares.IsSquareFilled(position);
        }
    }
}
