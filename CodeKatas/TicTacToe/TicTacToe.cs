using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodeKatas.TicTacToe
{
    public class TicTacToe
    {
        //public Dictionary<int, string> Board { get; set; }

        //public Board Board;
        public readonly Game Game;

        public TicTacToe()
        {
            Game = new Game();
        }

        public List<ValidationResult> TryTakeTurn(int position, string currentPlayer)
        {
            var validationResults = Game.CheckIfMoveValid(position, currentPlayer);

            if (validationResults.All(validationResult => validationResult.IsValid))
            {
                MakeMove(position, currentPlayer);
                return validationResults;
            }

            return validationResults;
        }

        private void MakeMove(int position, string currentPlayer)
        {
            Game.Board[position] = currentPlayer;
            Game.PreviousTurn = currentPlayer;
        }
    }
}

