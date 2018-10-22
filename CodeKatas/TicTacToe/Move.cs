namespace CodeKatas.TicTacToe
{
    public class Move
    {
        public BoardPosition Position { get; }
        public PlayerSign PlayerSign { get; }
    
        public Move(BoardPosition position, PlayerSign playerSign)
        {
            Position = position;
            PlayerSign = playerSign;
        }

        public override bool Equals(object obj)
        {
            var square = obj as Move;
            return square != null &&
                   Position == square.Position;
        }

        public override int GetHashCode()
        {
            var hashCode = -53491477;
            hashCode = hashCode * -1521134295 + Position.GetHashCode();
            return hashCode;
        }
    }
}
