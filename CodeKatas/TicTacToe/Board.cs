using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Board
    {
        private List<Move> _moves;
        private const int MaximumMoveCount = 9;
        public PlayerSign lastPlayed;

        public Board()
        {
            _moves = new List<Move>();
        }

        public void AddMoveToBoard(BoardPosition position, PlayerSign playerSign)
        {
            var move = new Move(position, playerSign);

            ValidateMove(playerSign, move);

            _moves.Add(move);

            lastPlayed = playerSign;
        }

        private void ValidateMove(PlayerSign playerSign, Move move)
        {
            _moves.CheckFirstPlayerCorrect(playerSign);
            _moves.CheckIfAllMovesTaken(MaximumMoveCount);
            _moves.CheckIfPositionTaken(move);

            move.CheckCorrectPlayer(lastPlayed);
        }

        public bool AreAllMovesTaken()
        {
            return _moves.Count() == MaximumMoveCount;
        }

        public PlayerSign GetPlayerSignOnBoardPosition(BoardPosition position)
        {
            var move = _moves.FirstOrDefault(e => e.Position == position);

            return move == null ? PlayerSign.Empty : move.PlayerSign;
        }
    }
}
