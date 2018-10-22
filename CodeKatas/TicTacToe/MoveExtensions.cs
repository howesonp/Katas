using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public static class MoveExtensions
    {
        public static void CheckFirstPlayerCorrect(this List<Move> moves, PlayerSign playerSign)
        {
            if (moves.Count == 0 && playerSign == PlayerSign.Nought)
            {
                throw new Exception("Nought cannot take the first move");
            }
        }

        public static void CheckIfPositionTaken(this List<Move> moves, Move thisMove)
        {
            if (moves.Any(move => move.Equals(thisMove)))
            {
                throw new Exception("This position has already been taken");
            }
        }

        public static void CheckIfAllMovesTaken(this List<Move> moves, int MaximumMoveCount)
        {
            if (moves.Count() == MaximumMoveCount)
            {
                throw new Exception("No more positions available on the board");
            } 
        }

        public static void CheckCorrectPlayer(this Move move, PlayerSign previousPlayer)
        {
            if (move.PlayerSign == previousPlayer)
            {
                throw new Exception($"{move.PlayerSign} took the previous turn");
            }
        }
    }
}
