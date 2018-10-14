using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas.TicTacToe
{
    public class Game
    {
        public bool IsFinished { get; set; }

        public bool HasWinner { get; set; }

        public bool IsDraw { get; set; }

        public bool IsFirstMove { get; set; }

        public Game()
        {
            Board = new Dictionary<int, string>
            {
                {1, String.Empty},
                {2, String.Empty},
                {3, String.Empty},
                {4, String.Empty},
                {5, String.Empty},
                {6, String.Empty},
                {7, String.Empty},
                {8, String.Empty},
                {9, String.Empty}
            };
            IsFinished = false;
            IsDraw = false;
            IsFirstMove = true;
            HasWinner = false;
        }

        public Dictionary<int, string> Board { get; }

        private const string Nought = "O";
        private const string Cross = "X";
        public string PreviousTurn = String.Empty;

        //public string[] players = { "X", "O" };

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
            if (Board.All(e => e.Value != string.Empty))
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
            if (!string.IsNullOrEmpty(Board[position]))
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
