using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    internal class Board
    {
        private List<Move> _moves;
        private const int MaximumMoveCount = 9;
        public PlayerSign lastPlayed;

        public Board()
        {
            _moves = new List<Move>();
        }

        public void AddMoveToBoard(Move move)
        {
            ValidateMove(move);
            _moves.Add(move);
            lastPlayed = move.PlayerSign;
        }

        public PlayerSign GetPlayerSignOnBoardPosition(BoardPosition position)
        {
            var move = _moves.FirstOrDefault(e => e.Position == position);

            return move == null ? PlayerSign.Empty : move.PlayerSign;
        }

        public GameState CheckForResult()
        {
            var isWin = CheckWinLines();

            if (isWin)
            {
                return lastPlayed == PlayerSign.Cross ? GameState.PlayerXWin : GameState.PlayerOWin;
            }

            return AreAllMovesTaken() ? GameState.IsDraw : GameState.InProgress;
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

        private void CheckIfAllMovesTaken(int MaximumMoveCount)
        {
            if (_moves.Count() == MaximumMoveCount)
            {
                throw new Exception("No more positions available on the board");
            }
        }

        private void CheckCorrectPlayer(Move move)
        {
            if (move.PlayerSign == lastPlayed)
            {
                throw new Exception($"{move.PlayerSign} took the previous turn");
            }
        }

        private bool AreAllMovesTaken()
        {
            return _moves.Count() == MaximumMoveCount;
        }

        private bool CheckWinLines()
        {
            var winningOptions = new GameWinOptions();

            return winningOptions.Lines.Select(winLine =>
                            GetPlayerSignOnBoardPosition(winLine[0]) == lastPlayed &&
                            GetPlayerSignOnBoardPosition(winLine[1]) == lastPlayed &&
                            GetPlayerSignOnBoardPosition(winLine[2]) == lastPlayed)
                    .Any(hasWin => hasWin);
        }
    }
}
