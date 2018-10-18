using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Game
    {
        public bool HasWinner { get; set; }

        public string WinningPlayer { get; set; }

        public bool IsDraw { get; set; }

        public bool IsFirstMove { get; set; }

        public Board Board { get; }

        public string PreviousTurn = string.Empty;

        public List<ValidationResult> MoveValidationResults { get; set; }

        public Game()
        {
            Board = new Board();
            IsDraw = false;
            IsFirstMove = true;
            HasWinner = false;
        }

        public void CheckIfMoveValid(int position, string currentPlayer)
        {
            MoveValidationResults = new List<ValidationResult>();

            if (IsFirstMove)
            {
                MoveValidationResults.Add(this.CheckFirstTurnIsX(currentPlayer));
                IsFirstMove = false;
            }

            MoveValidationResults.Add(this.CheckGameIsNotOver());
            MoveValidationResults.Add(this.CheckIfPositionAlreadyTaken(position));
            MoveValidationResults.Add(this.CheckCorrectPlayer(currentPlayer));
        }

        public void CheckForDraw()
        {
            IsDraw = Board.Squares.All(square => !string.IsNullOrEmpty(square.Value))
                     && !HasWinner;
        }

        public void CheckForWin()
        {
            var isWin = CheckHorizontalWinLines() ||
                        CheckVerticalLinesForWin() ||
                        CheckDiagonalLinesForWin();

            if (isWin)
            {
                HasWinner = true;
                WinningPlayer = PreviousTurn;
            }
        }

        private bool CheckHorizontalWinLines()
        {
            const int horizontalWinLineAddition = 1;
            return CheckWinLines(Board.WinningHorizontalLines, horizontalWinLineAddition);
        }

        private bool CheckVerticalLinesForWin()
        {
            const int verticalWinLineAddition = 3;
            return CheckWinLines(Board.WinningVerticalLines, verticalWinLineAddition);
        }

        private bool CheckDiagonalLinesForWin()
        {
            const int firstDiagonalLineAddition = 4;
            const int secondDiagonalLineAddition = 2;

            var firstDiagonalHasWin = CheckWinLines(Board.WinningDiagonalLineOne, firstDiagonalLineAddition);
            var secondDiagonalHasWin = CheckWinLines(Board.WinningDiagonalLineTwo, secondDiagonalLineAddition);

            return firstDiagonalHasWin || secondDiagonalHasWin;
        }

        private bool CheckWinLines(int[] winLines, int addition)
        {
            return winLines.Select(winLine => CheckLineForWin(winLine, addition)).Any(hasWin => hasWin);
        }

        private bool CheckLineForWin(int startOfWinLine, int addition)
        {
            return Board.Squares[startOfWinLine] == PreviousTurn &&
                   Board.Squares[startOfWinLine + addition] == PreviousTurn &&
                   Board.Squares[startOfWinLine + addition + addition] == PreviousTurn;
        }
    }
}
