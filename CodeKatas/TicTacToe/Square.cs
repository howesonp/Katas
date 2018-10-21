using System;

namespace CodeKatas.TicTacToe
{
    public class Square
    {
        public BoardPosition Position { get; set; }
        public PlayerSign PlayerSign { get; set; }


        //public override bool Equals(object obj) => obj is Square square &&
        //           Position == square.Position &&
        //           PlayerSign == square.PlayerSign;

        //public override int GetHashCode()
        //{
        //    var hashCode = -53491477;
        //    hashCode = hashCode * -1521134295 + Position.GetHashCode();
        //    hashCode = hashCode * -1521134295 + PlayerSign.GetHashCode();
        //    return hashCode;
        //}

        //protected bool Equals(Square other)
        //{
        //    return Position == other.Position && PlayerSign == other.PlayerSign;
        //}
    }
}
