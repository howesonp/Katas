using System;

namespace CodeKatas.TicTacToe
{
    public static class BoardExtensions
    {
        public static void CheckFirstPlayerIsNotNought(this Board board, PlayerSign currentPlayer)
        {
            if (board.AreAllSquaresEmpty() && currentPlayer == PlayerSign.Nought)
            {
                throw new Exception("O cannot go first");
            }
        }

        public static void PositionAlreadyTaken(this Board board, BoardPosition position)
        {
            if (board.IsSquareFilled(position))
            {
                throw new Exception("Position already taken");
            }
        }
    }
}
