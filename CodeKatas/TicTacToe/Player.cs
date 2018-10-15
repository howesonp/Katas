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

            return _game.HasWinner;
        }

        private bool CheckHorizontalWinLines()
        {
            var hasWin = false;

            foreach (var boardWinningHorizontalLine in _game.Board.WinningHorizontalLines)
            {
                hasWin = CheckLineForWin(boardWinningHorizontalLine);

                if (hasWin)
                {
                    return true;
                }
            }

            return hasWin;

        }

        private bool CheckLineForWin(int boardWinningHorizontalLine)
        {
            if (_game.Board.Squares[boardWinningHorizontalLine] == _playerSign &&
                _game.Board.Squares[boardWinningHorizontalLine + 1] == _playerSign &&
                _game.Board.Squares[boardWinningHorizontalLine + 2] == _playerSign)
            {
                return true;
            }

            return false;
        }

        private bool CheckVerticalLinesForWin()
        {
            return false;
        }

        private bool CheckDiagonalLinesForWin()
        {
            return false;
        }
    }
}
