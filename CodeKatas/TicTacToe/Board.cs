using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    internal class Board
    {
        private readonly List<Move> _moves;
        private const int MaximumMoveCount = 9;
        public PlayerSign LastPlayed;

        public Board()
        {
            _moves = new List<Move>();
        }

        public void AddMoveToBoard(Move move)
        {
            ValidateMove(move);
            _moves.Add(move);
            LastPlayed = move.PlayerSign;
        }

        public PlayerSign GetPlayerSignOnBoardPosition(BoardPosition position)
        {
            var move = _moves.FirstOrDefault(e => e.Position == position);

            return move?.PlayerSign ?? PlayerSign.Empty;
        }

        public GameState CheckForResult()
        {
            var isWin = CheckWinLines(LastPlayed);

            if (isWin)
            {
                return LastPlayed == PlayerSign.Cross ? GameState.PlayerXWin : GameState.PlayerOWin;
            }

            return _moves.Count() == MaximumMoveCount ? GameState.IsDraw : GameState.InProgress;
        }

        private bool CheckWinLines(PlayerSign playerSign)
        {
            return GameWinOptions.Lines.Select(winLine =>
                    GetPlayerSignOnBoardPosition(winLine[0]) == playerSign &&
                    GetPlayerSignOnBoardPosition(winLine[1]) == playerSign &&
                    GetPlayerSignOnBoardPosition(winLine[2]) == playerSign)
                .Any(hasWin => hasWin);
        }

        private void ValidateMove(Move move)
        {
            CheckFirstPlayerCorrect(move.PlayerSign);
            CheckIfAllMovesTaken(MaximumMoveCount);
            CheckIfPositionTaken(move);
            CheckCorrectPlayer(move);
        }

        private void CheckFirstPlayerCorrect(PlayerSign playerSign)
        {
            if (_moves.Count == 0 && playerSign == PlayerSign.Nought)
            {
                throw new Exception("Nought cannot take the first move");
            }
        }

        private void CheckIfPositionTaken(Move thisMove)
        {
            if (_moves.Any(move => move.Equals(thisMove)))
            {
                throw new Exception("This position has already been taken");
            }
        }

        private void CheckIfAllMovesTaken(int maximumMoveCount)
        {
            if (_moves.Count() == maximumMoveCount)
            {
                throw new Exception("No more positions available on the board");
            }
        }

        private void CheckCorrectPlayer(Move move)
        {
            if (move.PlayerSign == LastPlayed)
            {
                throw new Exception($"{move.PlayerSign} took the previous turn");
            }
        }
    }
}
