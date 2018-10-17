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
                CheckForWin();
                CheckForDraw();
            }
        }

        private void MakeMove(int position)
        {
            _game.Board.Squares[position] = _playerSign;
            _game.PreviousTurn = _playerSign;
        }

        private void CheckForWin()
        {
            var isWin = CheckHorizontalWinLines() ||
                        CheckVerticalLinesForWin() ||
                        CheckDiagonalLinesForWin();

            if (isWin)
            {
                _game.HasWinner = true;
                _game.WinningPlayer = _playerSign;
            }
        }

        private void CheckForDraw()
        {
            _game.IsDraw = _game.Board.Squares.All(square => !string.IsNullOrEmpty(square.Value))
                           && !_game.HasWinner;
        }

        private bool CheckLineForWin(int startOfWinLine, int addition)
        {
            return _game.Board.Squares[startOfWinLine] == _playerSign &&
                   _game.Board.Squares[startOfWinLine + addition] == _playerSign &&
                   _game.Board.Squares[startOfWinLine + addition + addition] == _playerSign;
        }

        private bool CheckHorizontalWinLines()
        {
            const int horizontalWinLineAddition = 1;
            return CheckWinLines(_game.Board.WinningHorizontalLines, horizontalWinLineAddition);
        }

        private bool CheckVerticalLinesForWin()
        {
            const int verticalWinLineAddition = 3;
            return CheckWinLines(_game.Board.WinningVerticalLines, verticalWinLineAddition);
        }

        private bool CheckDiagonalLinesForWin()
        {
            const int firstDiagonalLineAddition = 4;
            const int secondDiagonalLineAddition = 2;

            var firstDiagonalHasWin =  CheckWinLines(_game.Board.WinningDiagonalLineOne, firstDiagonalLineAddition);
            var secondDiagonalHasWin =  CheckWinLines(_game.Board.WinningDiagonalLineTwo, secondDiagonalLineAddition);

            return firstDiagonalHasWin || secondDiagonalHasWin;
        }

        private bool CheckWinLines(int[] winLines, int addition)
        {
            return winLines.Select(winLine => CheckLineForWin(winLine, addition)).Any(hasWin => hasWin);
        }
    }
}
