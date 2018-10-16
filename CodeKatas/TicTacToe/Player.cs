using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Player
    {
        private readonly string _playerSign;
        private readonly Game _game;

        public Player(string playerSign, Game game)
        {
            _playerSign = playerSign;
            _game = game;
        }

        public void TryToTakeTurn(int position)
        {
            _game.CheckIfMoveValid(position, _playerSign);

            if (_game.MoveValidationResults.All(validationResult => validationResult.IsValid))
            {
                MakeMove(position);

                if (!IsWinningMove()) return;

                _game.IsFinished = true;
                _game.HasWinner = true;
                _game.WinningPlayer = _playerSign;
            }
        }

        private void MakeMove(int position)
        {
            _game.Board.Squares[position] = _playerSign;

            _game.PreviousTurn = _playerSign;
        }

        private bool IsWinningMove()
        {
            return CheckHorizontalWinLines() ||
                              CheckVerticalLinesForWin() ||
                              CheckDiagonalLinesForWin();
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
