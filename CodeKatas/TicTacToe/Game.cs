using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        public List<ValidationResult> MoveValidationResults { get; set; }

        public Game()
        {
            Board = new Board();
            
            IsFinished = false;
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
