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
                CheckForWin();
                return validationResults;
            }

            return validationResults;
        }

        private void MakeMove(int position)
        {
            _game.Board.Squares[position] = _playerSign;
            _game.PreviousTurn = _playerSign;
        }

        public bool CheckForWin()
        {
            _game.HasWinner = CheckHorizontalWinLines() ||
                              CheckVerticalLinesForWin() ||
                              CheckDiagonalLinesForWin();

            return _game.HasWinner;
        }

        private bool CheckHorizontalWinLines()
        {
            return true;
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
