using System.Runtime.InteropServices;

namespace CodeKatas.TicTacToe
{
    public class Player
    {
        private readonly PlayerSign _playerSign;
        private readonly Game _game;

        public Player(PlayerSign playerSign, Game game)
        {
            _playerSign = playerSign;
            _game = game;
        }

        public void TryToTakeTurn(BoardPosition position)
        {
            _game.CheckIfMoveValid(position, _playerSign);

            MakeMove(position);
            _game.CheckForWin();
            _game.CheckForDraw();
        }

        private void MakeMove(BoardPosition position)
        {
            _game.Board.UpdateSquare(_playerSign, position);
            _game.PreviousTurn = _playerSign;
        }
    }
}
