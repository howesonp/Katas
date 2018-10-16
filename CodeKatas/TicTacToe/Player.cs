﻿using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Player
    {
        private readonly string _playerSign;
        private readonly Game _game;

        public Player(string playerSign, Game game)
        {
            this._playerSign = playerSign;
            this._game = game;
        }

        public List<ValidationResult> TryToTakeTurn(int position)
        {
            var validationResults = _game.CheckIfMoveValid(position, _playerSign);

            if (validationResults.All(validationResult => validationResult.IsValid))
            {
                MakeMove(position);

                return validationResults;
            }

            return validationResults;
        }

        private void MakeMove(int position)
        {
            _game.Board.Squares[position] = _playerSign;
            _game.PreviousTurn = _playerSign;
            var win = CheckForWin();
            if (win)
            {
                _game.IsFinished = true;
            }
        }

        private bool CheckForWin()
        {
            _game.HasWinner = CheckHorizontalWinLines() ||
                              CheckVerticalLinesForWin() ||
                              CheckDiagonalLinesForWin();

            _game.WinningPlayer = _playerSign;
            return _game.HasWinner;
        }

        private bool CheckHorizontalWinLines()
        {
            foreach (var boardWinningHorizontalLine in _game.Board.WinningHorizontalLines)
            {
                var hasWin = CheckLineForWin(boardWinningHorizontalLine, 1);

                if (hasWin)
                {
                    return true;
                }
            }

            return false;

        }

        private bool CheckLineForWin(int boardWinningHorizontalLine, int addition)
        {
            return _game.Board.Squares[boardWinningHorizontalLine] == _playerSign &&
                   _game.Board.Squares[boardWinningHorizontalLine + addition] == _playerSign &&
                   _game.Board.Squares[boardWinningHorizontalLine + addition + addition] == _playerSign;
        }

        private bool CheckVerticalLinesForWin()
        {
            foreach (var boardWinningHorizontalLine in _game.Board.WinningVerticalLines)
            {
                var hasWin = CheckLineForWin(boardWinningHorizontalLine, 3);

                if (hasWin)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckDiagonalLinesForWin()
        {
            return false;
        }
    }
}
