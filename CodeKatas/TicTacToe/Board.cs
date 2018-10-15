﻿using System.Collections.Generic;

namespace CodeKatas.TicTacToe
{
    public class Board
    {
        public Dictionary<int, string> Squares;

        public int[,] WinningHorizontalLines = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        public Board()
        {
            Squares = new Dictionary<int, string>
            {
                {1, string.Empty},
                {2, string.Empty},
                {3, string.Empty},
                {4, string.Empty},
                {5, string.Empty},
                {6, string.Empty},
                {7, string.Empty},
                {8, string.Empty},
                {9, string.Empty},
            };
        }
    }
}
