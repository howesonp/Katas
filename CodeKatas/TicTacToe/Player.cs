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
            _game.MakeMove(position, _playerSign);
            _game.CheckForResult(_playerSign);
        }
    }
}
