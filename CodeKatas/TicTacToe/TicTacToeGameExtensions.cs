using System;
using System.Linq;

namespace CodeKatas.TicTacToe
{
    public static class TicTacToeGameExtensions
    {
        public static ValidationResult CheckGameIsNotOver(this Game currentGame)
        {
            if (currentGame.Board.All(e => e.Value != string.Empty))
            {
                currentGame.IsFinished = true;
                return new ValidationResult {IsValid = false, ValidationMessage = "Game is over"};
            }

            return new ValidationResult{IsValid = true};
        }

        public static ValidationResult CheckFirstTurnIsX(this Game game, string currentPlayer)
        {
            if (game.IsFirstMove && currentPlayer == "O")
            {
                return new ValidationResult { IsValid = false, ValidationMessage = "O is not permitted to take the first turn" };
            }

            return new ValidationResult { IsValid = true };
        }

        public static ValidationResult CheckIfPositionAlreadyTaken(this Game game, int position)
        {
            if (!string.IsNullOrEmpty(game.Board[position]))
            {
                return new ValidationResult { IsValid = false, ValidationMessage = "Board position already filled" };
            }

            return new ValidationResult { IsValid = true };
        }

        public static ValidationResult CheckCorrectPlayer(this Game game , string currentPlayer)
        {
            if (game.PreviousTurn == currentPlayer)
            {
                return new ValidationResult { IsValid = false, ValidationMessage = $"{currentPlayer} took the previous turn" };
            }

            return new ValidationResult { IsValid = true };
        }
    }
}