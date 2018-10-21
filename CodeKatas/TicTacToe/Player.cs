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

        public GameState TryToTakeTurn(BoardPosition position)
        {
            _game.CheckIfMoveValid(position, _playerSign);

            _game.Board.UpdateSquare(_playerSign, position);
            _game.PreviousPlayer = _playerSign;

            return _game.CheckForResult(_playerSign);
        }
    }
}
