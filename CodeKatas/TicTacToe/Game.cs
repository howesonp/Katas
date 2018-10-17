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
                MoveValidationResults.Add(CheckFirstTurnIsX(currentPlayer));
                IsFirstMove = false;
            }

            MoveValidationResults.Add(CheckGameIsNotOver());
            MoveValidationResults.Add(CheckIfPositionAlreadyTaken(position));
            MoveValidationResults.Add(CheckCorrectPlayer(currentPlayer));
        }

        private ValidationResult CheckGameIsNotOver()
        {
            return HasWinner || IsDraw
                ? new ValidationResult { IsValid = false, ValidationMessage = "Game is over" } 
                : new ValidationResult {IsValid = true}; 
        }

        private ValidationResult CheckFirstTurnIsX(string currentPlayer)
        {
            return IsFirstMove && currentPlayer == "O"
                ? new ValidationResult {IsValid = false, ValidationMessage = "O is not permitted to take the first turn"}
                : new ValidationResult {IsValid = true};
        }

        private ValidationResult CheckIfPositionAlreadyTaken(int position)
        {
            return !string.IsNullOrEmpty(Board.Squares[position])
                ? new ValidationResult { IsValid = false, ValidationMessage = "Board position already filled" }
                : new ValidationResult {IsValid = true};
        }

        private ValidationResult CheckCorrectPlayer(string currentPlayer)
        {
            return PreviousTurn == currentPlayer 
                ? new ValidationResult { IsValid = false, ValidationMessage = $"{currentPlayer} took the previous turn" } 
                : new ValidationResult { IsValid = true };
        }
    }
}
