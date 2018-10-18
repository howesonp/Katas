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

            if (_game.MoveIsValid())
            {
                MakeMove(position);
                _game.CheckForWin();
                _game.CheckForDraw();
            }
        }

        private void MakeMove(int position)
        {
            _game.Board.Squares[position] = _playerSign;
            _game.PreviousTurn = _playerSign;
        }
    }
}
