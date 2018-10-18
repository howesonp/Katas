using System.Linq;
using System.Net;

namespace CodeKatas.TicTacToe
{
    public static class GameExtensions
    {
        public static bool MoveIsValid(this Game game)
        {
            return game.MoveValidationResults.All(validationResult => validationResult.IsValid);
        }

        public static ValidationResult CheckGameIsNotOver(this Game game)
        {
            return game.HasWinner || game.IsDraw
                ? new ValidationResult { IsValid = false, ValidationMessage = "Game is over" }
                : new ValidationResult { IsValid = true };
        }

        public static ValidationResult CheckFirstTurnIsX(this Game game, string currentPlayer)
        {
            return game.IsFirstMove && currentPlayer == "O"
                ? new ValidationResult { IsValid = false, ValidationMessage = "O is not permitted to take the first turn" }
                : new ValidationResult { IsValid = true };
        }

        public static ValidationResult CheckIfPositionAlreadyTaken(this Game game, int position)
        {
            return game.PositionAlreadyTaken(position)
                ? new ValidationResult { IsValid = false, ValidationMessage = "Board position already filled" }
                : new ValidationResult { IsValid = true };
        }

        public static ValidationResult CheckCorrectPlayer(this Game game, string currentPlayer)
        {
            return game.PreviousTurn == currentPlayer
                ? new ValidationResult { IsValid = false, ValidationMessage = $"{currentPlayer} took the previous turn" }
                : new ValidationResult { IsValid = true };
        }

        private static bool PositionAlreadyTaken(this Game game, int position)
        {
            return !string.IsNullOrEmpty(game.Board.Squares[position]);
        }
    }
}
