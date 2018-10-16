using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public class Game
    {
        public bool IsFinished { get; set; }

        public bool HasWinner { get; set; }

        public string WinningPlayer { get; set; }

        public bool IsDraw { get; set; }

        public bool IsFirstMove { get; set; }
    
        public Board Board { get; }

        public string PreviousTurn = string.Empty;

        public Game()
        {
            Board = new Board();
            
            IsFinished = false;
            IsDraw = false;
            IsFirstMove = true;
            HasWinner = false;
        }
        

        public List<ValidationResult> CheckIfMoveValid(int position, string currentPlayer)
        {
            var validationResults = new List<ValidationResult>();

            if (IsFirstMove)
            {
                validationResults.Add(CheckFirstTurnIsX(currentPlayer));
                IsFirstMove = false;
            }

            validationResults.Add(CheckGameIsNotOver());
            validationResults.Add(CheckIfPositionAlreadyTaken(position));
            validationResults.Add(CheckCorrectPlayer(currentPlayer));

            return validationResults; 
        }

        private ValidationResult CheckGameIsNotOver()
        {
            if (Board.Squares.All(e => e.Value != string.Empty))
            {
                IsFinished = true;
                return new ValidationResult { IsValid = false, ValidationMessage = "Game is over" };
            }

            return new ValidationResult { IsValid = true };
        }

        private ValidationResult CheckFirstTurnIsX(string currentPlayer)
        {
            if (IsFirstMove && currentPlayer == "O")
            {
                return new ValidationResult { IsValid = false, ValidationMessage = "O is not permitted to take the first turn" };
            }

            return new ValidationResult { IsValid = true };
        }

        private ValidationResult CheckIfPositionAlreadyTaken(int position)
        {
            if (!string.IsNullOrEmpty(Board.Squares[position]))
            {
                return new ValidationResult { IsValid = false, ValidationMessage = "Board position already filled" };
            }

            return new ValidationResult { IsValid = true };
        }

        private ValidationResult CheckCorrectPlayer(string currentPlayer)
        {
            if (PreviousTurn == currentPlayer)
            {
                return new ValidationResult { IsValid = false, ValidationMessage = $"{currentPlayer} took the previous turn" };
            }

            return new ValidationResult { IsValid = true };
        }
    }
}
