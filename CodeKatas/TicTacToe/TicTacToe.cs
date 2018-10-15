using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodeKatas.TicTacToe
{
    public class TicTacToe
    {
        public readonly Game Game;
        public Player PlayerX;
        public Player PlayerO;

        public TicTacToe()
        {
            Game = new Game();
            PlayerX = new Player("X", Game);
            PlayerO = new Player("O", Game);
        }
    }
}

